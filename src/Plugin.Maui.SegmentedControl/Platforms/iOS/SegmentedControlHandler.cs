﻿#if IOS
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace Plugin.Maui.SegmentedControl.Handlers;

public class SegmentedControlHandler : ViewHandler<SegmentedControl, UISegmentedControl>
{
    public static IPropertyMapper<SegmentedControl, SegmentedControlHandler> Mapper = new PropertyMapper<SegmentedControl, SegmentedControlHandler>(ViewMapper)
    {
        [nameof(SegmentedControl.IsEnabled)] = MapIsEnabled,
        [nameof(SegmentedControl.SelectedSegment)] = MapSelectedSegment,
        [nameof(SegmentedControl.TintColor)] = MapTintColor,
        [nameof(SegmentedControl.SelectedTextColor)] = MapSelectedTextColor,
        [nameof(SegmentedControl.TextColor)] = MapTextColor,
        [nameof(SegmentedControl.Children)] = MapChildren
    };    

    public SegmentedControlHandler() : base(Mapper)
    {
    }

    public SegmentedControlHandler(IPropertyMapper mapper) : base(mapper ?? Mapper)
    {
    }

    protected override UISegmentedControl CreatePlatformView()
    {
        var segmentControl = new UISegmentedControl();
        for (var i = 0; i < VirtualView.Children.Count; i++)
        {
            segmentControl.InsertSegment(VirtualView.Children[i].Text, i, false);
        }

        for (var i = 0; i < VirtualView.Children.Count; i++)
        {
            var child = VirtualView.Children[i];
            segmentControl.SetEnabled(child.IsEnabled && VirtualView.IsEnabled, i);
        }


        segmentControl.Enabled = VirtualView.IsEnabled;
        segmentControl.TintColor = VirtualView.IsEnabled ? VirtualView.TintColor.ToPlatform() : VirtualView.DisabledTintColor.ToPlatform();
        segmentControl.SetTitleTextAttributes(new UIStringAttributes() { ForegroundColor = VirtualView.SelectedTextColor.ToPlatform() }, UIControlState.Selected);
        segmentControl.SelectedSegment = VirtualView.SelectedSegment;
        return segmentControl;
    }

    protected override void ConnectHandler(UISegmentedControl platformView)
    {
        base.ConnectHandler(platformView);

        platformView.ValueChanged += PlatformView_ValueChanged;
    }

    protected override void DisconnectHandler(UISegmentedControl platformView)
    {
        base.DisconnectHandler(platformView);
        platformView.ValueChanged -= PlatformView_ValueChanged;
    }

    void PlatformView_ValueChanged(object sender, EventArgs e)
    {
        VirtualView.SelectedSegment = (int)PlatformView.SelectedSegment;
    }

    static void MapChildren(SegmentedControlHandler handler, SegmentedControl control)
    {
        UISegmentedControl segmentControl = handler.PlatformView;
        segmentControl.RemoveAllSegments();
        for (var i = 0; i < handler.VirtualView.Children.Count; i++)
        {
            segmentControl.InsertSegment(handler.VirtualView.Children[i].Text, i, false);
        }

        for (var i = 0; i < handler.VirtualView.Children.Count; i++)
        {
            var child = handler.VirtualView.Children[i];
            segmentControl.SetEnabled(child.IsEnabled, i);
        }
        segmentControl.SelectedSegment = handler.VirtualView.SelectedSegment;
    }

    static void MapTintColor(SegmentedControlHandler handler, SegmentedControl control)
    {
        handler.PlatformView.SelectedSegmentTintColor = control.IsEnabled 
            ? control.TintColor.ToPlatform() 
            : control.DisabledTintColor.ToPlatform();
    }

    static void MapSelectedSegment(SegmentedControlHandler handler, SegmentedControl control)
    {
        handler.PlatformView.SelectedSegment = control.SelectedSegment;
        control.SendValueChanged();
    }

    static void MapIsEnabled(SegmentedControlHandler handler, SegmentedControl control)
    {
        handler.PlatformView.Enabled = control.IsEnabled;
        handler.PlatformView.TintColor = control.IsEnabled 
            ? control.TintColor.ToPlatform() 
            : control.DisabledTintColor.ToPlatform();
    }

    static void MapSelectedTextColor(SegmentedControlHandler handler, SegmentedControl control)
    {
        handler.PlatformView.SetTitleTextAttributes(new UIStringAttributes() { ForegroundColor = control.SelectedTextColor.ToPlatform() }, UIControlState.Selected);        
    }

    static void MapTextColor(SegmentedControlHandler handler, SegmentedControl control)
    {
        handler.PlatformView.SetTitleTextAttributes(new UIStringAttributes() { ForegroundColor = control.TextColor.ToPlatform() }, UIControlState.Normal);
    }

}
#endif
