using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastQueryStock.ViewModels.Controls
{
    public class StockMenuItem : BaseViewModel
    {

        private ICommand _command;
        private string _content;

        public ICommand Command
        {
            get { return _command; }
            set
            {
                _command = value;
                NotifyPropertyChanged("Command");
            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                NotifyPropertyChanged("Content");
            }
        }

    }
}
