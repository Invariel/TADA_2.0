using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TADA_Advanced_Desktop_Alerts
{
    public class PlayingAlert
    {
        List<PlayingObject> playingObjects = new List<PlayingObject>();
        List<PlayingObject> completedObjects = new List<PlayingObject>();

        TADAAlertImage alertImage;
        TADAAlertAudio alertAudio;
        TADAAlertText alertText;
        TADAAlertText alertResponse;

        Graphics graphics = MainTADAForm.mainTADAForm.AnimationWindow.graphics;

        public PlayingAlert (TADAAlertImage alertImage, TADAAlertAudio alertAudio, TADAAlertText alertText, TADAAlertText alertResponse)
        {
            if (alertImage != null)
            {
                this.alertImage = alertImage;
                // And configure it.
                if (this.alertImage.IsVideo)
                {
                    this.playingObjects.Add(new PlayingElement(this, alertImage));
                }
                else
                {
                    Bitmap tempMap = new Bitmap(alertImage.Filename);
                    if (ImageAnimator.CanAnimate (tempMap))
                    {
                        this.playingObjects.Add(new PlayingAnimatedImage(this, alertImage));
                    }
                    else
                    {
                        this.playingObjects.Add(new PlayingObject(this, alertImage));
                    }
                    tempMap.Dispose();
                }
            }

            if (alertAudio != null)
            {
                this.alertAudio = alertAudio;
                this.playingObjects.Add(new PlayingElement(this, alertAudio));
            }

            if (alertText != null)
            {
                this.alertText = alertText;
                this.playingObjects.Add(new PlayingText(this, alertText));
            }

            if (alertResponse != null)
            {
                this.alertResponse = alertResponse;
                this.playingObjects.Add(new PlayingText(this, alertResponse));
            }
        }

        public void RemoveObject (PlayingObject playingObject)
        {
            if (!this.completedObjects.Contains(playingObject))
            {
                this.completedObjects.Add(playingObject);
                this.playingObjects.Remove(playingObject);
                MainTADAForm.mainTADAForm.AnimationWindow.InvalidateMe();
                CheckCompleted();
            }
        }

        public void CheckCompleted()
        {
            if (this.completedObjects.Intersect<PlayingObject>(this.playingObjects).Count() == this.playingObjects.Count())
            {
                MainTADAForm.mainTADAForm.AnimationWindow.AlertFinished(this);
                foreach (PlayingObject o in this.completedObjects)
                {
                    o.Dispose();
                }
            }
        }

        public void DrawElements (Graphics g)
        {
            foreach (PlayingObject o in this.playingObjects)
            {
                if (o is PlayingText && (o as PlayingText).BehindImage())
                {
                    o.DrawImage(g);
                }
            }

            foreach (PlayingObject o in this.playingObjects)
            {
                if (!(o is PlayingText))
                {
                    o.DrawImage(g);
                }
            }

            foreach (PlayingObject o in this.playingObjects)
            {
                if (o is PlayingText && !(o as PlayingText).BehindImage())
                {
                    o.DrawImage(g);
                }
            }
        }

        public void Play ()
        {
            List<PlayingObject> remainingObjects = new List<PlayingObject>();

            foreach (PlayingObject playableObject in this.playingObjects)
            {
                if (!this.completedObjects.Contains(playableObject))
                {
                    remainingObjects.Add(playableObject);
                }
            }

            foreach (PlayingObject o in remainingObjects)
            {
                o.Start();
            }
            MainTADAForm.mainTADAForm.AnimationWindow.InvalidateMe();
        }

        public void Stop ()
        {
            foreach (PlayingObject o in this.playingObjects)
            {
                o.Stop();
            }

            foreach (PlayingObject o in this.playingObjects)
            {
                o.Dispose(true);
            }

            MainTADAForm.mainTADAForm.AnimationWindow.InvalidateMe();
            this.CheckCompleted();
        }
    }

    // For playing a static image with no soundbyte.
    public class PlayingObject : IDisposable
    {
        public bool isDisposed;

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            this.positionTimer.Stop();
            this.positionTimer.Dispose();

            this.angleTimer.Stop();
            this.angleTimer.Dispose();

            this.magnificationTimer.Stop();
            this.magnificationTimer.Dispose();

            this.opacityTimer.Stop();
            this.opacityTimer.Dispose();
            
            if (this.mainImage != null)
            {
                this.mainImage.Dispose();
            }

            if (this.modifiedImage != null)
            {
                this.modifiedImage.Dispose();
            }

            this.stopwatch.Stop();

            this.containerAlert.RemoveObject(this);
            this.isDisposed = true;
        }

        public virtual void Stop()
        {

        }

        protected Bitmap mainImage;
        protected Bitmap modifiedImage;

        protected PointF position;
        protected PointF modifiedPosition;

        protected Size initialSize;

        protected ActiveEffect angleEffect = new ActiveEffect();
        protected ActiveEffect magnificationEffect = new ActiveEffect();
        protected ActiveEffect opacityEffect = new ActiveEffect();

        protected double angle = 0;
        protected double magnification = 1;
        protected double opacity = 1;

        protected PointF bezierP0;
        protected PointF bezierC0;
        protected PointF bezierC1;
        protected PointF bezierP1;

        protected double bezierTotalTime = 0;

        protected double fps = (MainTADAForm.mainTADAForm.rad_60FPS.Checked ? 60 : 30);  // The 60 FPS build for GGG.

        protected bool redrawMe = false;

        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        private Timer angleTimer = new Timer();
        private Timer magnificationTimer = new Timer();
        private Timer opacityTimer = new Timer();
        protected Timer positionTimer = new Timer();
        protected Timer persistTimer = new Timer();

        protected bool animationCompleted = false;

        protected double DrawAngle
        {
            get
            {
                return this.angle % 360;
            }
        }

        protected bool ReachedTarget (ActiveEffect effect, double value)
        {
            return effect.target == value;
        }

        protected bool ReachedDestination ()
        {
            return this.position == this.bezierP1 && this.stopwatch.ElapsedMilliseconds >= this.bezierTotalTime / 1000;
        }

        public void StartWatch ()
        {
            this.stopwatch.Start();
        }

        public void StopWatch ()
        {
            this.stopwatch.Stop();
        }

        protected void SetNextAngle(Effect nextAngle)
        {
            this.angle = (this.DrawAngle + 360) % 360;

            double targetAngle = nextAngle.target;

            Effect tempAngle = new Effect();
            tempAngle.Copy(nextAngle);
            tempAngle.target = targetAngle;

            SetNextEffect(this.angleEffect, tempAngle, this.angleTimer);
        }

        protected void SetNextEffect(ActiveEffect currentEffect, Effect nextEffect, Timer timer)
        {
            double delta = nextEffect.target - currentEffect.target;

            currentEffect.Copy(nextEffect);
            currentEffect.delta = delta / nextEffect.frames;
            timer.Interval = currentEffect.duration * 1000.0 / currentEffect.frames;
        }

        protected void SetNextDestination (Waypoint w)
        {
            this.bezierP0 = this.position;
            this.bezierP1 = w.P1;

            if (w.IsCurve)
            {
                this.bezierC0 = w.C0;
                this.bezierC1 = w.C1;
            }
            else
            {
                Bezier.CalculateLinearControlPoints(this.bezierP0, this.bezierP1, out this.bezierC0, out this.bezierC1);
            }

            this.bezierTotalTime = w.TransitionTime;

            this.stopwatch.Restart();
        }

        public void NextEffectFrame (ref double variable, ref ActiveEffect effect, Timer timer)
        {
            effect.NextEffectFrame(ref variable, timer);

            this.redrawMe = true;
            MainTADAForm.mainTADAForm.AnimationWindow.InvalidateMe();
        }

        protected void NextPosition ()
        {
            long ms = stopwatch.ElapsedMilliseconds;
            double bezierT = (Math.Min(ms / 1000.0d, this.bezierTotalTime) / this.bezierTotalTime);
            this.position = Bezier.CalculateBezierPosition(this.bezierP0, this.bezierC0, this.bezierC1, this.bezierP1, bezierT);
        }

        public virtual void DrawImage (Graphics g)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (this.redrawMe)
            {
                if (this.modifiedImage != null)
                {
                    this.modifiedImage.Dispose();
                }

                Size size = new Size(Convert.ToInt32(Math.Floor(this.initialSize.Width * this.magnification)),
                                     Convert.ToInt32(Math.Floor(this.initialSize.Height * this.magnification)));
                this.modifiedImage = new Bitmap(this.mainImage, size);

                this.redrawMe = false;
            }

            this.modifiedPosition = this.position;

            if (this.angleEffect.change && this.angleEffect.aroundCenter)
            {
                this.modifiedPosition.X += this.modifiedImage.Width / 2;
                this.modifiedPosition.Y += this.modifiedImage.Height / 2;
            }

            g.TranslateTransform(this.modifiedPosition.X, this.modifiedPosition.Y);
            g.RotateTransform((float)this.DrawAngle);

            PointF destinationPoint = new PointF(0, 0);
            if (this.angleEffect.change && this.angleEffect.aroundCenter)
            {
                destinationPoint.X = -this.modifiedImage.Width / 2;
                destinationPoint.Y = -this.modifiedImage.Height / 2;
            }

            if (this.opacity == 1)
            {
                g.DrawImage(this.modifiedImage, destinationPoint);
            }
            else
            {
                System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix()
                {
                    Matrix33 = (float)(this.opacity / 100.0)
                };

                System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);

                g.DrawImage(this.modifiedImage,
                            new Rectangle(Convert.ToInt32(Math.Floor(destinationPoint.X)),
                                          Convert.ToInt32(Math.Floor(destinationPoint.Y)),
                                          this.modifiedImage.Width,
                                          this.modifiedImage.Height),
                            0,
                            0,
                            this.modifiedImage.Width,
                            this.modifiedImage.Height,
                            GraphicsUnit.Pixel,
                            imageAttributes);
            }

            g.RotateTransform((float)(-this.DrawAngle));
            g.TranslateTransform(-this.modifiedPosition.X, -this.modifiedPosition.Y);
        }

        protected PlayingAlert containerAlert;
        protected TADAAlertImage alertImage;
        protected WaypointContainer waypoints;
        protected Graphics graphics;

        public bool BehindImage()
        {
            return this.waypoints.CurrentWaypoint != null && this.waypoints.CurrentWaypoint.BehindImage;
        }

        public PlayingObject(PlayingAlert containerAlert, TADAAlertImage alertImage)
        {
            this.containerAlert = containerAlert;
            this.graphics = MainTADAForm.mainTADAForm.AnimationWindow.graphics;

            if (alertImage != null)
            {
                this.mainImage = new Bitmap(alertImage.Filename);
                this.alertImage = alertImage;
                this.waypoints = alertImage.Waypoints;
                this.initialSize = alertImage.Dimensions;
            }

            this.angleTimer.Elapsed += new ElapsedEventHandler((sender, e) => { this.NextEffectFrame(ref this.angle, ref this.angleEffect, this.angleTimer); });
            this.magnificationTimer.Elapsed += new ElapsedEventHandler((sender, e) => { this.NextEffectFrame(ref this.magnification, ref this.magnificationEffect, this.magnificationTimer); });
            this.opacityTimer.Elapsed += new ElapsedEventHandler((sender, e) => { this.NextEffectFrame(ref this.opacity, ref this.opacityEffect, this.opacityTimer); });

            this.positionTimer.Interval = 1000.0 / this.fps;
            this.positionTimer.Elapsed += new ElapsedEventHandler((sender, e) =>
            {
                Task.Run(() =>
                {
                    this.NextPosition();
                    MainTADAForm.mainTADAForm.AnimationWindow.InvalidateMe();
                });

                if (this.ReachedDestination())
                {
                    positionTimer.Stop();

                    if (this.waypoints.CurrentWaypoint.PersistTime > 0)
                    {
                        this.persistTimer.Interval = this.waypoints.CurrentWaypoint.PersistTime * 1000;
                        this.persistTimer.Start();
                    }
                    else
                    {
                        this.Persist();
                    }
                }
            });

            this.persistTimer.Elapsed += new ElapsedEventHandler((sender, e) => { this.Persist(); });

            this.redrawMe = true;
        }

        public virtual void Start ()
        {
            this.waypoints.FirstWaypoint();
            this.stopwatch.Start(); // Filthy hack to just use restart () all the time.
            if (this.waypoints.CurrentWaypoint != null)
            {
                this.ConfigureWaypoint();
            }
        }

        protected virtual void ConfigureWaypoint()
        {
            this.positionTimer.Stop();
            Waypoint w = this.waypoints.CurrentWaypoint;

            if (this.waypoints.IsFirstWaypoint())
            {
                // If it's the first waypoint, move to P1
                this.position = this.waypoints.CurrentWaypoint.P1;
            }

            if (w.Angle.change)
            {
                this.SetNextAngle(w.Angle); // Doesn't use SetNextEffect directly because angle needs to be normalized first.
                this.angleTimer.Start();
            }

            if (w.Magnification.change)
            {
                this.SetNextEffect(this.magnificationEffect, w.Magnification, this.magnificationTimer);
                this.magnificationTimer.Start();
            }

            if (w.Opacity.change)
            {
                this.SetNextEffect(this.opacityEffect, w.Opacity, this.opacityTimer);
                this.opacityTimer.Start();
            }

            if (w.TransitionTime > 0 && !this.waypoints.IsFirstWaypoint())
            {
                this.SetNextDestination(w);
                this.fps = 30.0;
                this.positionTimer.Start();
            }
            else if (this.waypoints.IsFirstWaypoint() || w.TransitionTime == 0)
            {
                if (w.PersistTime > 0)
                {
                    this.persistTimer.Interval = w.PersistTime * 1000;
                    this.persistTimer.Start();
                }
                else
                {
                    this.Persist();
                }
            }
        }

        protected virtual void Persist()
        {
            this.persistTimer.Stop();

            Waypoint w = this.waypoints.CurrentWaypoint;
            
            // Press keys, if this is a press keys sorta situation.
            if (w != null && !String.IsNullOrEmpty(w.WindowTarget) && !String.IsNullOrEmpty(w.Keypresses))
            {
                if (!KeyHandler.SendKeyToProgram(w.WindowTarget, w.Keypresses))
                {
                    General.LogFile(String.Format("[{0}]: Tried to send {1} to {2} but was unable.  Is it running?", DateTime.Now, w.Keypresses, w.WindowTarget));
                }
            }

            this.waypoints.NextWaypoint();
            if (this.waypoints.CurrentWaypoint == null)
            {
                this.animationCompleted = true;
                positionTimer.Dispose();
                this.Dispose();
            }
            else
            {
                this.ConfigureWaypoint();
                if (w.TransitionTime > 0)
                {
                    this.positionTimer.Start();
                }
            }
        }
    }

    // For playing an animated gif with no soundbyte.
    public class PlayingAnimatedImage : PlayingObject
    {
        private Bitmap[] imageFrames;
        private int[] imageDelays;

        private int currentFrame = -1;
        private int remainingAnimationLoops = 0;

        private Timer frameTimer = new Timer();

        public Bitmap CurrentImage
        {
            get { return this.imageFrames[currentFrame]; }
        }

        public int CurrentDelay
        {
            get { return this.imageDelays[currentFrame]; }
        }

        public void NextFrame()
        {
            this.frameTimer.Stop();

            ++this.currentFrame;
            if (this.currentFrame >= this.imageFrames.Length)
            {
                if (this.remainingAnimationLoops == 0)
                {
                    this.currentFrame = this.imageFrames.Length - 1;
                    this.remainingAnimationLoops = 0;
                    this.frameTimer.Dispose();
                    return;
                }
                else
                {
                    --this.remainingAnimationLoops;
                    this.currentFrame = 0;
                }
            }

            if (this.remainingAnimationLoops < 0)
            {
                this.currentFrame = this.imageFrames.Length - 1;
                this.remainingAnimationLoops = 0;
            }

            this.mainImage = this.CurrentImage;
            this.frameTimer.Interval = this.CurrentDelay;
            this.frameTimer.Start();
            this.redrawMe = true;
            MainTADAForm.mainTADAForm.AnimationWindow.InvalidateMe();
        }

        public PlayingAnimatedImage(PlayingAlert containerAlert, TADAAlertImage alertImage) : base (containerAlert, alertImage)
        {
            Bitmap b = new Bitmap(alertImage.Filename);

            General.ExtractAllFrames(b, alertImage.Dimensions, out this.imageFrames, out this.imageDelays, out int throwawayTime);
            this.remainingAnimationLoops = alertImage.AnimationLoops;

            this.frameTimer.Elapsed += new ElapsedEventHandler((sender, e) =>
            {
                this.NextFrame();
            });
        }

        public override void Start()
        {
            base.Start();
            this.NextFrame();
        }
    }

    // For playing both videos and audio files.
    public class PlayingElement : PlayingObject
    {
        private System.Windows.Forms.Integration.ElementHost elementHost;
        private System.Windows.Controls.MediaElement mediaElement;

        private String mediaFile;

        private TimeSpan startingPosition;
        private TimeSpan endingPosition;
        private TimeSpan delay;
        private TimeSpan duration;

        private Timer balanceTimer = new Timer();
        private Timer volumeTimer = new Timer();

        private ActiveEffect balanceEffect = new ActiveEffect();
        private ActiveEffect volumeEffect = new ActiveEffect();

        private double balance = 0;
        private double volume = 0;

        private bool isPlaying = false;

        public override void Dispose()
        {
            if (this.TestComplete())
            {
                base.Dispose();
            }
        }

        public override void Dispose(bool disposing)
        {
            this.mediaElement.Dispatcher.Invoke(() => { MainTADAForm.mainTADAForm.AnimationWindow.Controls.Remove(this.elementHost); });

            balanceTimer.Stop();
            volumeTimer.Stop();

            base.Dispose(disposing);
        }

        public PlayingElement(PlayingAlert containerAlert, TADAAlertImage alertImage) : base(containerAlert, null)
        {
            this.waypoints = alertImage.Waypoints;

            this.mediaFile = alertImage.Filename;

            this.startingPosition = alertImage.StartingPosition;
            this.endingPosition = alertImage.EndingPosition;
            this.delay = alertImage.Delay;
            this.duration = alertImage.Duration;

            this.balance = alertImage.Balance;
            this.volume = alertImage.Volume;

            this.initialSize = alertImage.Dimensions;

            this.SharedSetup();
        }

        public PlayingElement(PlayingAlert containerAlert, TADAAlertAudio alertAudio) : base(containerAlert, null)
        {
            this.waypoints = alertAudio.Waypoints;

            this.mediaFile = alertAudio.Filename;

            this.startingPosition = alertAudio.StartingPosition;
            this.endingPosition = alertAudio.EndingPosition;
            this.delay = alertAudio.Delay;
            this.duration = alertAudio.Duration;

            this.balance = alertAudio.Balance;
            this.volume = alertAudio.Volume;

            this.initialSize = new Size(0, 0);

            this.SharedSetup();
        }

        private void SharedSetup()
        {
            MainTADAForm.mainTADAForm.AnimationWindow.Invoke((Action)(() =>
            {
                this.elementHost = new System.Windows.Forms.Integration.ElementHost();
                this.mediaElement = new System.Windows.Controls.MediaElement();

                this.elementHost.Child = this.mediaElement;

                this.elementHost.Width = this.initialSize.Width;
                this.elementHost.Height = this.initialSize.Height;
                this.mediaElement.Width = this.initialSize.Width;
                this.mediaElement.Height = this.initialSize.Height;

                this.mediaElement.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
                this.mediaElement.Source = new Uri(this.mediaFile);
                this.mediaElement.Balance = this.balance;
                this.mediaElement.Volume = this.volume;
            }));

            // Waypoint volume is from 0 to 100, while mediaElement volume is from 0 to 1.
            this.volume /= 100.0;

            this.balanceTimer.Elapsed += new ElapsedEventHandler((sender, e) =>
            {
                this.NextEffectFrame(ref this.balance, ref this.balanceEffect, this.balanceTimer);
                this.mediaElement.Dispatcher.Invoke(() => { this.mediaElement.Balance = this.balance; });
            });

            this.volumeTimer.Elapsed += new ElapsedEventHandler((sender, e) =>
            {
                this.NextEffectFrame(ref this.volume, ref this.volumeEffect, this.volumeTimer);
                this.mediaElement.Dispatcher.Invoke(() => { this.mediaElement.Volume = this.volume / 100.0; });
            });
        }

        public async override void Start()
        {
            this.isPlaying = true;

            if (this.delay.Ticks > 0)
            {
                await (Task.Delay(this.delay));
            }

            this.mediaElement.Play();
            this.mediaElement.Pause();

            MainTADAForm.mainTADAForm.AnimationWindow.Invoke((Action)(() => { MainTADAForm.mainTADAForm.AnimationWindow.Controls.Add(this.elementHost); }));

            this.waypoints.FirstWaypoint();

            if (this.waypoints.CurrentWaypoint != null)
            {
                this.position = this.waypoints.CurrentWaypoint.P1;
            }
            else
            {
                this.position = new Point(-1, -1);
            }

            this.redrawMe = false;
            this.DrawImage(MainTADAForm.mainTADAForm.AnimationWindow.graphics);

            TimeSpan playDuration = this.endingPosition.Subtract(this.startingPosition);
            base.Start();   // Start the waypoints and such.
            do
            {
                TimeSpan thisRun = (this.duration > playDuration) ? playDuration : this.duration;
                this.duration = this.duration.Subtract(thisRun);

                this.mediaElement.Position = this.startingPosition;
                this.mediaElement.Play();
                await (Task.Delay(thisRun));
            } while (this.duration.Ticks > 0);

            MainTADAForm.mainTADAForm.AnimationWindow.Invoke((Action)(() =>
            {
                this.mediaElement.Stop();
                this.mediaElement.ClearValue(System.Windows.Controls.MediaElement.SourceProperty);
                this.isPlaying = false;
                this.Dispose();
            }));
        }
        
        public override void Stop()
        {
            MainTADAForm.mainTADAForm.AnimationWindow.Invoke((Action)(() =>
            {
                this.mediaElement.Stop();
                this.mediaElement.ClearValue(System.Windows.Controls.MediaElement.SourceProperty);
                this.isPlaying = false;
                this.Dispose();
            }));
        }

        protected override void ConfigureWaypoint()
        {
            this.positionTimer.Stop();
            Waypoint w = this.waypoints.CurrentWaypoint;

            if (w != null)
            {
                if (w.Balance.change)
                {
                    this.SetNextEffect(this.balanceEffect, w.Balance, this.balanceTimer);
                    this.balanceTimer.Start();
                }

                if (w.Volume.change)
                {
                    this.SetNextEffect(this.volumeEffect, w.Volume, this.volumeTimer);
                    this.volumeTimer.Start();
                }
            }

            base.ConfigureWaypoint();
        }

        public override void DrawImage(Graphics g)
        {
            if (this.redrawMe)
            {
                int width = Convert.ToInt32(Math.Floor(this.initialSize.Width * this.magnification));
                int height = Convert.ToInt32(Math.Floor(this.initialSize.Height * this.magnification));

                this.elementHost.Width = width;
                this.elementHost.Height = height;
                this.mediaElement.Width = width;
                this.mediaElement.Height = height;

                this.redrawMe = false;
            }

            if (this.elementHost.InvokeRequired)
            {
                this.elementHost.Invoke((Action)(() =>
                {
                    this.elementHost.Location = new System.Drawing.Point(Convert.ToInt32(Math.Floor(this.position.X)), Convert.ToInt32(Math.Floor(this.position.Y)));
                    this.elementHost.PerformLayout();
                }));
            }
            else
            {
                this.elementHost.Location = new System.Drawing.Point(Convert.ToInt32(Math.Floor(this.position.X)), Convert.ToInt32(Math.Floor(this.position.Y)));
                this.elementHost.PerformLayout();
            }
        }

        private bool TestComplete()
        {
            return !this.isPlaying && this.waypoints.CurrentWaypoint == null;
        }
    }

    // Next up, PlayingText - the code that handles the Twitch streamer and Twitch user text.
    public class PlayingText : PlayingObject
    {
        private System.Speech.Synthesis.SpeechSynthesizer talky;
        private System.Speech.Synthesis.PromptBuilder prompt;

        private bool talkingCompleted = true;

        public PlayingText(PlayingAlert containerAlert, TADAAlertText alertText) : base(containerAlert, null)
        {
            this.waypoints = alertText.Waypoints;

            if (!String.IsNullOrEmpty(alertText.Message))
            {
                this.mainImage = General.GenerateMessageBitmap(alertText.Foreground, alertText.Background, alertText.Outline, alertText.ChosenFont, alertText.OutlineWidth, alertText.Dimensions, alertText.Padding, alertText.GetMessage());
            }

            if (alertText.TTSVolume > 0 && !String.IsNullOrEmpty(alertText.TTSVoice) && !String.IsNullOrEmpty(alertText.GetReadMessage()))
            {
                talky = new System.Speech.Synthesis.SpeechSynthesizer();
                try
                {
                    talky.SelectVoice(alertText.TTSVoice);

                    prompt = new System.Speech.Synthesis.PromptBuilder();
                    prompt.AppendText(alertText.GetReadMessage());

                    talky.SpeakCompleted += (sender, e) =>
                    {
                        this.talkingCompleted = true;
                        this.Dispose();
                    };
                }
                catch (Exception)
                {
                    talky.Dispose();
                    talky = null;
                }
            }

            if (this.mainImage != null)
            {
                this.initialSize = this.mainImage.Size;
            }
        }

        protected override void Persist()
        {
            this.persistTimer.Stop();

            if (this.waypoints.CurrentWaypoint != null && this.waypoints.CurrentWaypoint.ReadMessage && this.talky != null && this.prompt != null)
            {
                this.talkingCompleted = false;
                this.talky.SpeakAsync(this.prompt);
            }

            base.Persist();
        }

        // Need to not dispose of this until SpeakCompleted.
        public override void Dispose()
        {
            if (this.talkingCompleted)
            {
                talky?.Dispose();
            }

            if (this.animationCompleted)
            {
                this.mainImage?.Dispose();
                this.modifiedImage?.Dispose();
            }

            if (this.talkingCompleted && this.animationCompleted)
            {
                base.Dispose();
            }
        }
    }
}
