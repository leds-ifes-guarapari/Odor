﻿using Odor.Services;
using Odor.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Odor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : MasterDetailPage
    {
        private readonly MapsViewModel MapsViewModel;
        private readonly OdorViewModel OdorViewModel;
        private readonly UserViewModel UserViewModel;
        public MenuPage()
        {
            InitializeComponent();
            this.MapsViewModel = new MapsViewModel();
            this.OdorViewModel = new OdorViewModel();
            BindingContext = this.UserViewModel = new UserViewModel();
            Detail = new NavigationPage(new MasterPage(this.MapsViewModel));
            MessagingCenter.Subscribe<string, string>(this, "Message", async (Title, Message) =>
            {
                await DisplayAlert(Title, Message, "Ok");
            });
            MessagingCenter.Subscribe<string>(this, "Odor", async (Id) =>
            {
                if (string.IsNullOrEmpty(this.UserViewModel.User.Id))
                {
                    await Detail.Navigation.PushAsync(new UserPage(this.UserViewModel.User));
                }
                else
                {
                    await Detail.Navigation.PushAsync(
                        new OdorPage(
                            this.OdorViewModel.Odors.Where(element => element.Id.Equals(Id)).FirstOrDefault() ??
                            new Models.Odor
                            {
                                UserId = this.UserViewModel.User.Id,
                                Intensity = ConfigurationManager.Configuration.OdorIntensity,
                                Type = ConfigurationManager.Configuration.OdorType,
                                Latitude = this.MapsViewModel.Position.Latitude,
                                Longitude = this.MapsViewModel.Position.Longitude,
                                Address = (string.IsNullOrWhiteSpace(this.UserViewModel.User.Address)) ? ConfigurationManager.Configuration.OdorAddress : this.UserViewModel.User.Address,
                                Date = DateTime.Today,
                                Begin = DateTime.Now.TimeOfDay,
                                End = DateTime.Now.TimeOfDay
                            })
                        );
                }
                IsPresented = false;
            });
            MessagingCenter.Subscribe<string>(this, "User", async (Id) =>
            {
                if (string.IsNullOrEmpty(Id))
                {
                    await Detail.Navigation.PushAsync(new UserPage(this.UserViewModel.User));
                    IsPresented = false;
                }
                else
                {
                    if (this.OdorViewModel.Odors.Count == 0)
                    {
                        MessagingCenter.Send(Id, "QueryOdor");
                    }
                }
            });
            MessagingCenter.Send(string.Empty, "GetUser");
        }
        private async void GoUserPage(object sender, EventArgs args)
        {
            await Detail.Navigation.PushAsync(new UserPage(this.UserViewModel.User));
            IsPresented = false;
        }
        private async void GoOdorPage(object sender, EventArgs args)
        {
            await Detail.Navigation.PopToRootAsync();
            IsPresented = false;
        }
        private async void GoListPage(object sender, EventArgs args)
        {
            await Detail.Navigation.PushAsync(new ListPage(this.OdorViewModel));
            IsPresented = false;
        }
        private async void GoAboutPage(object sender, EventArgs args)
        {
            await Detail.Navigation.PushAsync(new AboutPage());
            IsPresented = false;
        }
    }
}