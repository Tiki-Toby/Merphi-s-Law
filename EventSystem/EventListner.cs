using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairyEngine.EventSystem
{
	public interface IEventListener { }
	public interface IEventListener<T> : IEventListener
	{
		void ExecuteEvent(T eventType);
	}
}
