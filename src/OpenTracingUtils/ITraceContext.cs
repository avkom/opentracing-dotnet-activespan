using OpenTracing;

namespace OpenTracingUtils
{
	/// <summary>
	/// <para>Allows users to propagate spans with its <see cref="ISpanContext"/> in-process
	/// by using AsyncLocal storage.</para>
	/// </summary>
	public interface ITraceContext
	{
		/// <summary>
		/// Gets the latest span from the local execution storage without removing it.
		/// </summary>
		ISpan CurrentSpan { get; }

		/// <summary>
		/// Adds the given span to the local execution storage.
		/// </summary>
		void Push(ISpan span);

		/// <summary>
		/// Removes and returns the lastest span from the local execution storage.
		/// </summary>
		void TryPop();
	}
}
