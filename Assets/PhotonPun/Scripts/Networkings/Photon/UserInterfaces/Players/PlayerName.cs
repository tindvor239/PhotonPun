namespace Photon.Pun.Interfaces
{
    public class PlayerName : WorldUIDisplay
    {
        protected override void Awake()
        {
            textName.text = photonView.Owner.NickName;
        }
    }
}