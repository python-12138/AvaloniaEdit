using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace AvaloniaEdit.Demo.测试文件夹
{
    public class A : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action CommandParameter { get; set; }

        public A(object commandParameter)
        {
            this.CommandParameter = commandParameter as Action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.CommandParameter();
        }
    }
     
        
      
    
}
