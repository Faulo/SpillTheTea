using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GossipGang {
    sealed class ApplicationInfo : MonoBehaviour {
        TMP_Text component;
        string template;

        void Awake() {
            if (TryGetComponent(out component)) {
                template = component.text;
            }
        }

        void Start() {
            UpdateText();
        }

        void OnEnable() {
            GameManager.onChangeDays += UpdateText;
        }

        void OnDisable() {
            GameManager.onChangeDays -= UpdateText;
        }

        IEnumerable<(string, object)> tokens {
            get {
                yield return ("Application.name", Application.productName);
                yield return ("Application.version", Application.version);
                yield return ("Game.dayCount", GameManager.instance.dayCount);

                if (GameManager.state is not null) {
                    yield return ("Game.currentRound", GameManager.state.currentRound);
                    yield return ("Game.lastRound", GameManager.state.lastRound);
                    yield return ("Game.firstPlayer", string.Join(", ", GameManager.state.firstPlayers.Select(p => p.nameWithColor)));
                    yield return ("Game.secretPlayer", string.Join(", ", GameManager.state.playersWithSecretsRemoved.Select(p => p.nameWithColor)));
                    yield return ("Game.playerCount", GameManager.state.playerCount);
                }
            }
        }

        void UpdateText() {
            if (component) {
                component.text = ReplaceTokens(template);
            }
        }

        string ReplaceTokens(string text) {
            foreach (var (key, value) in tokens) {
                text = text.Replace($"{{{key}}}", value.ToString());
            }

            return text;
        }
    }
}
