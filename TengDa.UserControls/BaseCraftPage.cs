using System.Threading.Tasks;

namespace TengDa.UserControls
{
    public class BaseCraftPage : BasePage
    {
        public virtual int CraftDID { get; set; }

        public virtual string CraftNO { get; set; }

        //public override int? GetFacilityState()
        //{
        //    return this.GetGetFacilityStateByCraft(this.CraftDID);
        //}
    }
}
