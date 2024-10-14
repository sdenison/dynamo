using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Security
{
    public class SystemPassphrases
    {
        public readonly List<Passphrase> Passphrases;

        public SystemPassphrases(List<string> passphrases)
        {
            Passphrases = new List<Passphrase>();
            foreach (var passphraseString in passphrases)
            {
                Passphrases.Add(new Passphrase(passphraseString));
            }
        }

        public int ValidPassphraseCount()
        {
            return Passphrases.Count(x => x.IsValid());
        }

        public int ValidPassphraseCountNoAnagrams()
        {
            return Passphrases.Count(x => x.IsValidNoAnagrams());
        }
    }
}
