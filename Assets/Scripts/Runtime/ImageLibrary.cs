﻿using UnityEngine;

namespace GossipGang {
    [CreateAssetMenu]
    sealed class ImageLibrary : ScriptableAsset {
        [SerializeField]
        Sprite image;

        Sprite[] images;
        public Sprite LookUp(string name) {
            images ??= Resources.LoadAll<Sprite>("");

            foreach (var image in images) {
                if (image.name == name) {
                    return image;
                }
            }

            return image;
        }
    }
}