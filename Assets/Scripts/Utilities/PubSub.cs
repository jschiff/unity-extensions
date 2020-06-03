using UnityEngine;
using System;
using System.Collections.Generic;
using Com.Jschiff.UnityExtensions.Extensions;

namespace Com.Jschiff.UnityExtensions.Utilities {

    public class PubSub {
        public static PubSub Instance { get; } = new PubSub();

        Dictionary<Topic, HashSet<Subscription>> subscriptions = new Dictionary<Topic, HashSet<Subscription>>();

        public Subscription Subscribe(Topic topic, Delegate del) {
            if (!del.GetType().Equals(topic.delegateType)) {
                throw new Exception("Invalid delegate type");
            }

            var subs = subscriptions.ComputeIfAbsent(topic, () => new HashSet<Subscription>());
            var subscription = new Subscription(topic, del);
            subs.Add(subscription);

            return subscription;
        }

        // Return true if the subscription was successfully removed. False if it did not exist.
        public bool Unsubscribe(Subscription subscription) {
            if (subscriptions.TryGetValue(subscription.topic, out HashSet<Subscription> subs)) {
                return subs.Remove(subscription);
            }

            return false;
        }

        public void Broadcast(Topic topic, params object[] args) {
            if (subscriptions.TryGetValue(topic, out HashSet<Subscription> subs)) {
                foreach (var sub in subs) {
                    sub.Invoke(args);
                }
            }
        }
    }

    public class Topic {
        internal readonly Type delegateType;

        public Topic(Type delegateType) {
            this.delegateType = delegateType;
        }

        public Subscription Subscribe(Delegate del) {
            return PubSub.Instance.Subscribe(this, del);
        }

        public bool Unsubscribe(Subscription subscription) {
            return PubSub.Instance.Unsubscribe(subscription);
        }
    }

    public class Subscription {
        internal readonly Delegate del;
        internal readonly Topic topic;

        public Subscription(Topic topic, Delegate del) {
            this.del = del;
            this.topic = topic;
        }

        public void Invoke(object[] args) {
            del.DynamicInvoke(args);
        }

        public bool Unsubscribe() {
            return topic.Unsubscribe(this);
        }
    }
}
