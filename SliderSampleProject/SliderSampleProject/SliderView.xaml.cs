using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SliderSampleProject {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SliderView : ContentView {
		public SliderView() {
			InitializeComponent();
		}

		private void ContentView_SizeChanged(object sender, EventArgs e) {
			BoxBackground.CornerRadius = Height / 2;
			BoxSliderBackground.CornerRadius = (Height - BoxSliderBackground.Margin.VerticalThickness) / 2;
		}

		private void FrmCircle_SizeChanged(object sender, EventArgs e) {
			FrmCircle.WidthRequest = FrmCircle.Height;
			FrmCircle.CornerRadius = (float)(FrmCircle.Height / 2);
		}

		public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(SliderView), Color.FromHex("#e3e3e3"), propertyChanged: OnBackgroundColorChanged);
		public new Color BackgroundColor {
			get { return (Color)GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
		}

		private static void OnBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue) {
			if(bindable is SliderView view) {
				if(view.BoxBackground != null) {
					view.BoxBackground.Color = (Color)newValue;
				}
			}
		}

		public static readonly BindableProperty SliderBackgroundColorProperty = BindableProperty.Create(nameof(SliderBackgroundColor), typeof(Color), typeof(SliderView), Color.FromHex("#4c9cf5"), propertyChanged: OnSliderBackgroundColorChanged);
		public Color SliderBackgroundColor {
			get { return (Color)GetValue(SliderBackgroundColorProperty); }
			set { SetValue(SliderBackgroundColorProperty, value); }
		}

		private static void OnSliderBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue) {
			if(bindable is SliderView view) {
				if(view.BoxSliderBackground != null) {
					view.BoxSliderBackground.Color = (Color)newValue;
				}
			}
		}

		public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(CornerRadius), typeof(string), typeof(SliderView), "", propertyChanged: OnTextPropertyChanged);
		public string Text {
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
			if(bindable is SliderView view) {
				if(view.Lbl != null) {
					view.Lbl.Text = (string)newValue;
				}
			}
		}

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(SliderView), Color.Black, propertyChanged: OnTextColorChanged);
		public Color TextColor {
			get { return (Color)GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		private static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue) {
			if(bindable is SliderView view) {
				if(view.Lbl != null) {
					view.Lbl.TextColor = (Color)newValue;
				}
			}
		}

		public static readonly BindableProperty SliderWidthRequestProperty = BindableProperty.Create(nameof(SliderWidthRequest), typeof(double), typeof(SliderView), 100.0, propertyChanged: OnSliderWidthRequestPropertyChanged);
		public double SliderWidthRequest {
			get { return (double)GetValue(SliderWidthRequestProperty); }
			set { SetValue(SliderWidthRequestProperty, value); }
		}

		private static void OnSliderWidthRequestPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
			if(bindable is SliderView view) {
				view.ColSlider.Width = new GridLength((double)newValue);
			}
		}

		public static readonly BindableProperty ExecuteProperty = BindableProperty.Create(nameof(Execute), typeof(EventHandler), typeof(SliderView), null);
		public event EventHandler Execute;

		double panX = 0;
		private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e) {
			switch(e.StatusType) {
				case GestureStatus.Started:
					panX = FrmCircle.TranslationX;
					break;
				case GestureStatus.Running:
					double x = panX + ((float)e.TotalX);
					FrmCircle.TranslationX = x < 0 ? 0 : x > BoxSliderBackground.Width - FrmCircle.Width ? BoxSliderBackground.Width - FrmCircle.Width : x;
					if(x >= BoxSliderBackground.Width - FrmCircle.Width) {
						IsEnabled = false;
						Execute?.Invoke(this, new EventArgs());
						Task.Run(async () => {
							await Task.Delay(500);
							await FrmCircle.TranslateTo(0, 0, 150, Easing.SinOut);
							IsEnabled = true;
						});
					}
					break;
				case GestureStatus.Completed:
				case GestureStatus.Canceled:
					if(FrmCircle.TranslationX < BoxSliderBackground.Width - FrmCircle.Width) {
						FrmCircle.TranslateTo(0, 0, 150, Easing.SinOut);
					}
					break;
			}
		}

	}
}