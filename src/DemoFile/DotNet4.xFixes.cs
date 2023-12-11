using Priority_Queue;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Reserved to be used by the compiler for tracking metadata.
    /// This class should not be used by developers in source code.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit
    {
    }
}

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Used to indicate to the compiler that the <c>.locals init</c> flag should not be set in method headers.
    /// </summary>
    /// <remarks>Internal copy of the .NET 5 attribute.</remarks>
    [AttributeUsage(
        AttributeTargets.Module |
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Interface |
        AttributeTargets.Constructor |
        AttributeTargets.Method |
        AttributeTargets.Property |
        AttributeTargets.Event,
        Inherited = false)]
    internal sealed class SkipLocalsInitAttribute : Attribute
    {
    }
}

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>Specifies that an output will not be null even if the corresponding type allows it. Specifies that an input argument was not null when the call returns.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
    internal sealed class NotNullAttribute : Attribute { }

    /// <summary>Specifies that when a method returns <see cref="ReturnValue"/>, the parameter may be null even if the corresponding type disallows it.</summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class MaybeNullWhenAttribute : Attribute
    {
        /// <summary>Initializes the attribute with the specified return value condition.</summary>
        /// <param name="returnValue">
        /// The return value condition. If the method returns this value, the associated parameter may be null.
        /// </param>
        public MaybeNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;

        /// <summary>Gets the return value condition.</summary>
        public bool ReturnValue { get; }
    }

#if NETFRAMEWORK

    //
    // Summary:
    //     Specifies that when a method returns System.Diagnostics.CodeAnalysis.NotNullWhenAttribute.ReturnValue,
    //     the parameter will not be null even if the corresponding type allows it.
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class NotNullWhenAttribute : Attribute
    {
        //
        // Summary:
        //     Gets the return value condition.
        public bool ReturnValue { get; }

        //
        // Summary:
        //     Initializes the attribute with the specified return value condition.
        //
        // Parameters:
        //   returnValue:
        //     The return value condition. If the method returns this value, the associated
        //     parameter will not be null.
        public NotNullWhenAttribute(bool returnValue)
        {
            ReturnValue = returnValue;
        }
    }

    /// <summary>Applied to a method that will never return under any circumstance.</summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
#if INTERNAL_NULLABLE_ATTRIBUTES
    internal
#else
    public
#endif
        sealed class DoesNotReturnAttribute : Attribute
    { }

#endif // NETFRAMEWORK
}

namespace System.Collections.Generic
{
    public class PriorityQueue<TElement, TPriority>
    {
        readonly SimplePriorityQueue<TElement, TPriority> m_queue = new();

        public int Count => m_queue.Count;

        public PriorityQueue()
        {
        }

        public PriorityQueue(int capacity)
        {
        }

        public PriorityQueue(IEnumerable<(TElement Element, TPriority Priority)> items)
        {
            foreach (var item in items)
            {
                m_queue.Enqueue(item.Element, item.Priority);
            }
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            m_queue.Enqueue(element, priority);
        }

        public bool TryDequeue(out TElement element, out TPriority priority)
        {
            if (!m_queue.TryFirst(out element))
            {
                priority = default!;
                return false;
            }

            priority = m_queue.GetPriority(element);

            m_queue.Dequeue();

            return true;
        }

        public TElement Dequeue()
        {
            return m_queue.Dequeue();
        }

        public bool TryPeek(out TElement element, out TPriority priority)
        {
            if (!m_queue.TryFirst(out element))
            {
                priority = default!;
                return false;
            }

            priority = m_queue.GetPriority(element);

            return true;
        }
    }
}

public static class DotNet4FixExtensions
{
    public static int EnsureCapacity<T>(this List<T> list, int capacity)
    {
        if (list.Capacity < capacity)
            list.Capacity = capacity;

        return list.Capacity;
    }
}

namespace DemoFile
{
    public static class BitOperations
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RoundUpToPowerOf2(uint value)
        {
            --value;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            return value + 1;
        }
    }
}

namespace System.Diagnostics
{
    public sealed class UnreachableException : Exception
    {
    }
}
