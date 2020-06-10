using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SliderSampleProject {
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();
		}

		private void SliderView_Execute(object sender, EventArgs e) {
			DisplayAlert("Execute", "Do stuff", "Close");
		}
	}
}
