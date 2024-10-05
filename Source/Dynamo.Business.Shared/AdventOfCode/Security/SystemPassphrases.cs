using System.Collections.Generic;
using System.Linq;

namespace Dynamo.Business.Shared.AdventOfCode.Security
{
    public class SystemPassphrases
    {
        private readonly List<Passphrase> _passphrases;

        public SystemPassphrases(List<string> passphrases)
        {
            _passphrases = new List<Passphrase>();
            foreach (var passphraseString in passphrases)
            {
                _passphrases.Add(new Passphrase(passphraseString));
            }
        }

        public int ValidPassphraseCount()
        {
            return _passphrases.Count(x => x.IsValid());
        }

        public int ValidPassphraseCountNoAnagrams()
        {
            return _passphrases.Count(x => x.IsValidNoAnagrams());
        }
    }
}
