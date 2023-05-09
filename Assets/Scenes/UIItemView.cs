public class UIItemView : UIView
{
    public object model;

    public UIItemView Clone()
    {
        var itemView = Instantiate(this, this.transform.parent);
        itemView.gameObject.SetActive(true);
        return itemView;
    }
}