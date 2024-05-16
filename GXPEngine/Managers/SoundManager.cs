using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public static class SoundManager
    {
        private static string prefix = "Assets/Sound/";
        private static Dictionary<string, Sound[]> soundEffects = new Dictionary<string, Sound[]>
        {
            {
                "accept",
                new Sound[]
                {
                    new Sound(prefix + "accept.wav")
                }
            },
            {
                "circleBump",
                new Sound[]
                {
                    new Sound(prefix + "circleBump1.wav"),
                    new Sound(prefix + "circleBump2.wav"),
                    new Sound(prefix + "circleBump3.wav")
                }
            },
            {
                "deniedAccess",
                new Sound[]
                {
                    new Sound(prefix + "deniedAccess.wav"),
                }
            },
            {
                "goBack",
                new Sound[]
                {
                    new Sound(prefix + "goBack.wav"),
                }
            },
            {
                "losePoints",
                new Sound[]
                {
                    new Sound(prefix + "losePoints.wav"),
                }
            },
            {
                "PG-2_Balling",
                new Sound[]
                {
                    new Sound(prefix + "PG-2_Balling.wav"),
                }
            },
            {
                "selectionChange",
                new Sound[]
                {
                    new Sound(prefix + "selectionChange.wav"),
                }
            },
            {
                "speedDown",
                new Sound[]
                {
                    new Sound(prefix + "speedDown1.wav"),
                    new Sound(prefix + "speedDown2.wav"),
                }
            },
            {
                "speedUp",
                new Sound[]
                {
                    new Sound(prefix + "speedUp1.wav"),
                    new Sound(prefix + "speedUp2.wav"),
                }
            },
            {
                "triangleBump",
                new Sound[]
                {
                    new Sound(prefix + "triangleBump1.wav"),
                    new Sound(prefix + "triangleBump2.wav"),
                    //new Sound(prefix + "triangleBump3.wav"),
                }
            },
            {
                "wallBump",
                new Sound[]
                {
                    new Sound(prefix + "wallHit1.wav"),
                    new Sound(prefix + "wallHit2.wav"),
                    new Sound(prefix + "wallHit3.wav"),
                }
            },
            {
                "GameMusic",
                new Sound[]
                {
                    new Sound(prefix + "PG-2_Balling.wav", true, true),
                }
            },
            {
                "MenuMusic",
                new Sound[]
                {
                    new Sound(prefix + "CinematicMainMenu.wav", true, true),
                }
            },
        };

        public static SoundChannel PlaySound(string sound, bool randomize = false)
        {
            Sound[] soundEffectList = soundEffects[sound];

            Sound soundEffect = soundEffectList[Utils.Random(0, soundEffectList.Length)];

            SoundChannel sc = soundEffect.Play();
            if(randomize)
                sc.Frequency = Utils.Random(30000, 48200);
            Console.WriteLine("played " + sound);
            return sc;
        }
    }
}
