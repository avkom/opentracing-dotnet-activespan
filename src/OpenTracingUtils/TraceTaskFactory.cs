using System.Threading.Tasks;

namespace OpenTracingUtils
{
    public class TraceTaskFactory : TaskFactory
    {
	    public TraceTaskFactory(TraceTaskScheduler taskScheduler)
			: base(taskScheduler)
	    {
	    }
    }
}
