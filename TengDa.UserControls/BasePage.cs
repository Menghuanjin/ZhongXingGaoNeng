using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TengDa.UserControls
{
    public class BasePage : Page
    {
        public BasePage()
        {
           this.Loaded += new RoutedEventHandler(this.BasePage_Loaded);
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //this.RefreshFacilityState();
        }

        //protected void RefreshFacilityState()
        //{
        //    this.UpdateFacilityState(this.GetFacilityState());
        //}

        //private void RefreshFacilityStateTimer_Tick(object sender, EventArgs e)
        //{
        //    this.UpdateFacilityState(this.GetFacilityState());
        //}

        //public void UpdateFacilityState(int? state)
        //{
        //    if (Application.Current.MainWindow == null || !(Application.Current.MainWindow is IFacilityStateWindow))
        //        return;
        //    ((IFacilityStateWindow)Application.Current.MainWindow).UpdateFicilityState(state);
        //}

        //public virtual int? GetFacilityState()
        //{
        //    return this.GetFacilityStateByProduction();
        //}

        //public int? GetFacilityStateByProduction()
        //{
        //    ProductionStateGetResponse stateGetResponse = LocalApi.Execute(new ProductionStateGetRequest());
        //    return stateGetResponse.IsError ? new int?() : new int?(stateGetResponse.State);
        //}

        //public int? GetGetFacilityStateByCraft(int craftDID)
        //{
        //    CraftStateGetResponse stateGetResponse = LocalApi.Execute(new CraftStateGetRequest() { CraftDID = craftDID });
        //    return stateGetResponse.IsError ? new int?() : new int?(stateGetResponse.State);

        //}
        public bool IsInDesignMode()
        {
            return DesignerProperties.GetIsInDesignMode(this);
        }
    }
}
