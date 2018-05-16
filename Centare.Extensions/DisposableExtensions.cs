// Copyright (c) 2018 Centare

using System;

namespace Centare.Extensions
{
    public static class DisposableExtensions
    {
        public static TResult Using<T, TResult>(this T disposable, Func<T, TResult> useFunc)
            where T : IDisposable
        {
            TResult result;
            using (disposable)
            {
                result = useFunc(disposable);
            }
            return result;
        }

        public static void Using<T>(this T disposable, Action<T> useAction)
            where T : IDisposable
        {
            using (disposable)
            {
                useAction(disposable);
            }
        } 
    }
}