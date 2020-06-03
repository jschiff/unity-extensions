using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Com.Jschiff.UnityExtensions.Utilities {
    public class AssetReferenceHelper {
        public delegate void BatchLoadingCompletedHandler(AssetReference[] failures);

        public static void LoadAll(IEnumerable<AssetReference> references, BatchLoadingCompletedHandler handler) {
            IDictionary<AssetReference, AsyncOperationHandle> stillLoading = new ConcurrentDictionary<AssetReference, AsyncOperationHandle>();
            ConcurrentQueue<AssetReference> failures = new ConcurrentQueue<AssetReference>();
            HashSet<AssetReference> dedup = new HashSet<AssetReference>(references);

            int added = 0;
            foreach (var reference in dedup) {
                if (reference.Asset == null) {
                    added++;
                    var handle = reference.LoadAssetAsync<object>();
                    stillLoading.Add(reference, handle);
                    handle.Completed += (AsyncOperationHandle<object> h) => {
                        if (h.Status != AsyncOperationStatus.Succeeded) {
                            failures.Enqueue(reference);
                        }

                        stillLoading.Remove(reference);
                        if (stillLoading.Count == 0) {
                            handler(failures.ToArray());
                        }
                        //else Debug.Log($"{stillLoading.Count} remaining items to load.");
                    };
                }
            }

            Debug.Log($"Loading {added} items");
            if (added == 0) {
                handler(failures.ToArray());
            }
        }
    }
}
