/*  (c) 2019 matthew fairchild
    simple demo grabbable object that changes color on all events and can be picked up and released
 */

using EightBitDinosaur;
using UnityEngine;

public class DemoGrabale : Grabable
{
    public override void on_overlap_grip_pressed(MotionController n_hand)
    {
        base.on_overlap_grip_pressed(n_hand);
        change_color(Resources.Load<Material>("Black"));
    }

    public override void on_overlap_grip_released(MotionController n_hand)
    {
        base.on_overlap_grip_released(n_hand);
        change_color(Resources.Load<Material>("Blue"));
    }

    public override void on_overlap_start(MotionController n_hand)
    {
        base.on_overlap_start(n_hand);
        change_color(Resources.Load<Material>("Green"));
    }

    public override void on_overlap_end(MotionController n_hand)
    {
        base.on_overlap_end(n_hand);
        change_color(Resources.Load<Material>("Orange"));
    }

    public override void on_focus_start(MotionController n_hand)
    {
        base.on_focus_start(n_hand);
        change_color(Resources.Load<Material>("Purple"));
    }

    public override void on_focus_end(MotionController n_hand)
    {
        base.on_focus_end(n_hand);
        change_color(Resources.Load<Material>("Red"));
    }

    public override void on_focus_trigger_pressed(MotionController n_hand)
    {
        base.on_focus_trigger_pressed(n_hand);
        change_color(Resources.Load<Material>("White"));
    }

    public override void on_focus_trigger_released(MotionController n_hand)
    {
        base.on_focus_trigger_released(n_hand);
        change_color(Resources.Load<Material>("Yellow"));
    }

    private void change_color(Material mat)
    {
        this.GetComponent<MeshRenderer>().material = mat;
    }
}
