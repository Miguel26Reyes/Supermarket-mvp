using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket_mvp.Models;
using Supermarket_mvp.Views;
using Supermarket_mvp._Repositories;

namespace Supermarket_mvp.Presenters
{
    internal class MainPresenter
    {
        private readonly IMainView mainView;
        private readonly string sqlconnectionstring;


        public MainPresenter(IMainView mainView, string sqlconnectionstring)
        {
            this.mainView = mainView;
            this.sqlconnectionstring = sqlconnectionstring;
        }

        public void ShowPayModeView(object? sender, EventArgs e) 
        {
            IPayModeView view = PayModeView.GetInstance((MainView)mainView);
            IPayModeRepository repository = new PayModeRepository(sqlconnectionstring);
            new PayModePresenter(view, repository);
        }

    }
}
