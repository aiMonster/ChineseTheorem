using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChineseTheoremMobileMVVM.Behaviors
{
    public class EntryBehavior: Behavior<Entry>
    {
        //private const 

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += bindable_TextChanged;
        }

        void bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            if(entry.Text.Contains("."))
            {
                entry.Text = entry.Text.Replace(".", "");                
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= this.bindable_TextChanged;
            base.OnDetachingFrom(bindable);            
        }

        
    }
}
