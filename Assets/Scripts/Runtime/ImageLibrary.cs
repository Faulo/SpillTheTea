﻿using UnityEngine;

namespace GossipGang {
    [CreateAssetMenu]
    sealed class ImageLibrary : ScriptableAsset {
        [SerializeField]
        string path;
        [SerializeField]
        Sprite defaultImage;

        Sprite[] images;
        public Sprite LookUp(string name) {
            images ??= Resources.LoadAll<Sprite>(path);

            foreach (var image in images) {
                if (image.name == name) {
                    return image;
                }
            }

            return defaultImage;
        }
    }
}
