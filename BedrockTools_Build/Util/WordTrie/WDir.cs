using System;
using System.Collections.Generic;
using System.Text;

namespace BedrockTools_Build.Util.WordTrie {
    public class WDir<T> {
        public Dictionary<string, WDir<T> > subDirs;
        public Dictionary<string, T> items;

        public WDir() {
            subDirs = new Dictionary<string, WDir<T>>();
            items = new Dictionary<string, T>();
        }

        public WDir<T> MoveDir(string dir) {
            if (!subDirs.ContainsKey(dir)) {
                AddDir(dir);
            }
            return subDirs[dir];
        }

        
        public void AddDir(string dir) {
            if (items.ContainsKey(dir)) {
                throw new WordTrieException($"Error, created a dir: {dir}, but it existed as an item");
            }
            subDirs.Add(dir, new WDir<T>());
        }

        public void AddItem(string item, T val) {
            if (subDirs.ContainsKey(item)) {
                throw new WordTrieException($"Error, created an item: {item}, but it existed as an dir");
            }
            items.Add(item, val);

        }

    }
}
