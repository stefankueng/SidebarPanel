# SidebarPanel for UWP
this is a SidebarPanel control for UWP, allowing to slide windows/controls to slide out from the side (left or right) and also allows docking.

![alt text](https://raw.githubusercontent.com/stefankueng/SidebarPanel/master/animation.gif "SidebarPanel animation")


example use:


```xaml
<Grid
    Background="{ThemeResource SystemControlPageBackgroundChromeLowBrush}">
    <usercontrols:SidebarPanel>
        <usercontrols:SidebarPanel.MainContent>
            <Grid>
                <RichEditBox />
            </Grid>
        </usercontrols:SidebarPanel.MainContent>

        <usercontrols:SidebarPanel.LeftContent>
            <usercontrols:SidebarItem Title="Left Panel 1" Background="Beige" >
                <TextBlock Text="this is the left panel 1 content text" />
            </usercontrols:SidebarItem>
            <usercontrols:SidebarItem Title="Left Panel 2">
                <TextBlock Text="this is the left panel 2 content text" />
            </usercontrols:SidebarItem>
        </usercontrols:SidebarPanel.LeftContent>

        <usercontrols:SidebarPanel.RightContent>
            <usercontrols:SidebarItem Title="Right Panel 1">
                <TextBlock Text="this is the right panel 1 content text" />
            </usercontrols:SidebarItem>
            <usercontrols:SidebarItem Title="Right Panel 2">
                <TextBlock Text="this is the right panel 2 content text" />
            </usercontrols:SidebarItem>
        </usercontrols:SidebarPanel.RightContent>
    </usercontrols:SidebarPanel>
</Grid>
```