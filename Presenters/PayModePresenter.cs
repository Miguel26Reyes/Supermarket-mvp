using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket_mvp.Views;
using Supermarket_mvp.Models;

namespace Supermarket_mvp.Presenters
{
    internal class PayModePresenter
    {
        private IPayModeView view;
        private IPayModeRepository repository;
        private BindingSource payModeBindingSource = new BindingSource();
        private IEnumerable<PayModeModel> PayModeList;
        private object payModeList;

        public int SavePayMode { get; }

        public PayModePresenter(IPayModeView view, IPayModeRepository repository) 
        {
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchPayMode;
            this.view.AddNewEvent += AddNewPayMode;
            this.view.EditEvent += LoadSelectPayModeToEdit;
            this.view.DeleteEvent += DeleteSelectedPayMode;
            this.view.SaveEvent += SavePaymode;
            this.view.CancelEvent += CancelAction;

            this.view.SetPayModeListBidingSource(payModeBindingSource);

            LoadAllPayModelist();

            this.view.Show();
        }

        private void LoadAllPayModelist()
        {
            payModeList = repository.GetAll();
            payModeBindingSource.DataSource = payModeList;
        }

        private void SearchPayMode(object? sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
            {
                payModeList = repository.GetByValue(this.view.SearchValue);
            }
            else
            {
                payModeList = repository.GetAll();
            }
            payModeBindingSource.DataSource = payModeList;
        }

        private void SavePaymode(object? sender, EventArgs e)
        {
            var payMode = new PayModeModel();
            payMode.Id = string.IsNullOrWhiteSpace(view.PayModeId) ? 0 : Convert.ToInt32(view.PayModeId);
            payMode.Name = view.PayModeName;
            payMode.Observation = view.PayModeObservation;

            try 
            {
                if (view.IsEdit) 
                {
                    repository.Edit(payMode);
                    view.Message = "PayMode edited succesfuly";
                }
                else 
                {
                    repository.Add(payMode);
                    view.Message = "ApyMode addes succesfuly";
                }
            }
            catch(Exception ex) 
            {
                view.IsSuccessful = true;
                LoadAllPayModelist();
                CleanViewFields();
            }

        }

        private void CleanViewFields()
        {
            view.PayModeId = "0";
            view.PayModeName = "";
            view.PayModeObservation = "";
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void DeleteSelectedPayMode(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddNewPayMode(object? sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        private void LoadSelectPayModeToEdit(object? sender, EventArgs e)
        {
            var payMode = (PayModeModel)payModeBindingSource.Current;

            view.PayModeId = payMode.Id.ToString();
            view.PayModeName = payMode.Name;
            view.PayModeObservation = payMode.Observation;

            view.IsEdit = true;
        }

        
       
    }
}
