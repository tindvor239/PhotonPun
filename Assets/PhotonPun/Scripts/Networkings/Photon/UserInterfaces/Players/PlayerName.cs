namespace Photon.Pun.Interfaces
{
    public class PlayerName : WorldUIDisplay
    {
        protected override void Awake()
        {
            base.Awake();
            textName.text = photonView.Owner.NickName;
        }
    }
}