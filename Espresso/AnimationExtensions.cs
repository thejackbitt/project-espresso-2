using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Animations;

public static class AnimationExtensions
{
    public static Task AnimateProperty(this VisualElement target, Action<double> propertySetter, double start, double end, uint duration, Easing easing)
    {
        var tcs = new TaskCompletionSource<bool>();

        var animation = new Microsoft.Maui.Controls.Animation(v => propertySetter(v), start, end);

        animation.Commit(target, "SimpleAnimation", 16, duration, easing, (v, c) => tcs.SetResult(true));

        return tcs.Task;
    }
}
