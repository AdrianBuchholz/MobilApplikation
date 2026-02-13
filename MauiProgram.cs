<?xml version="1.0" encoding="UTF-8" ?>
<Shell
    x:Class="MyMauiApp.AppShell"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:MyMauiApp"
    Title="MyMauiApp">

    <ShellContent
        Title="Home"
        ContentTemplate="{DataTemplate local:MainPage}"
        Route="MainPage" />

    <ShellContent
        Title="Bookings"
        ContentTemplate="{DataTemplate local:BookingsPage}"
        Route="BookingsPage" />
</Shell>

<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:vm="clr-namespace:MyMauiApp.ViewModels"
             x:Class="MyMauiApp.MainPage">

    <StackLayout Padding="10">
        <Label Text="Concerts" FontSize="24" HorizontalOptions="Center" />
        <CollectionView ItemsSource="{Binding Concerts}" SelectionMode="Single" SelectedItem="{Binding SelectedConcert, Mode=TwoWay}" x:Name="ConcertCollection">
            <CollectionView.ItemTemplate>
                <DataTemplate>
                    <StackLayout Padding="10">
                        <Label Text="{Binding Title}" FontAttributes="Bold" />
                        <Label Text="{Binding Description}" FontSize="12" />
                        <Label Text="{Binding BookingCount, StringFormat='Bookings: {0}'}" FontSize="12" />
                    </StackLayout>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>
        <Button Text="Load" Command="{Binding LoadCommand}" />
        <Button Text="My Bookings" Clicked="OnBookingsClicked" />
    </StackLayout>
</ContentPage>