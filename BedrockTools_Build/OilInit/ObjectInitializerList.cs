using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;

namespace BedrockTools_Build.OilInit {
    public class ObjectInitializerList : IEnumerable<KeyValuePair<string, ObjectInitializer> > {
        protected Dictionary<string, ObjectInitializer> objectInitializers;     
        public ObjectInitializerList(Dictionary<string, ObjectInitializer> list) {
            objectInitializers = list;           
        }

        public ObjectInitializer this [string key] {
            get => objectInitializers[key];
        }
        public string[] GetKeysNames () {
            return objectInitializers.Select(initializer => initializer.Key).ToArray();
        }

        public KeyValuePair<string, ObjectInitializer>[] GetInitializers() {
            return objectInitializers.ToArray();
        }
        public void Update(ObjectInitializerList otherList) {
            foreach(KeyValuePair<string, ObjectInitializer> item in otherList) {
                if (objectInitializers.ContainsKey(item.Key)) {
                    objectInitializers[item.Key].Update(
                        item.Value.GetParameters(), item.Value.GetObjectValues()
                    );
                } else {
                    throw new Exception(); //TODO: Throw proper exception
                }

            }
        }
        public IEnumerator<KeyValuePair<string, ObjectInitializer>> GetEnumerator() {
            return objectInitializers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }
}
