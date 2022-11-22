using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace PSI_MobileApp.Extensions
{
    public static class EntityFrameworkQueryableExtensions
    {
        public static async Task<ObservableCollection<T>> ToObservableCollecionAsync<T>(this IQueryable<T> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();
            await foreach (T item in source.AsAsyncEnumerable().WithCancellation(cancellationToken))
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}
