using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Extensions
{
    public static class FunctionalExtensions
    {
        public static TState Fold<T, TState>(this IEnumerable<T> seq, TState state, Func<T, TState, TState> operation)
        {
            if (seq == null) return state;
            foreach (var item in seq)
            {
                // eg. str = str + item
                state = operation(item, state);
            }
            return state;
        }
    }
}
