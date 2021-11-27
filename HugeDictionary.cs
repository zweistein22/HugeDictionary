using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rokin
{
   
    public class HugeDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>  where TKey : notnull
    {
        protected int arraysize = 512;
        protected int singlearraymaxitems = 20000000; // 20 millions x 
        protected Dictionary<TKey, TValue>[] _dictionaries;

        public HugeDictionary(IEqualityComparer<TKey>? comparer=null){

           
            _dictionaries = new Dictionary<TKey, TValue>[arraysize];

            for(int i=0; i< arraysize; i++)
            {
                _dictionaries[i] = new Dictionary<TKey, TValue>(comparer);
            }
        }

        public KeyValuePair<TKey, TValue> ElementAt(long index) {

            int i = (int)(index / singlearraymaxitems);
            int count = (int)(index % singlearraymaxitems);
            if (i >= arraysize || i == arraysize-1 && count == singlearraymaxitems - 1) throw new System.IndexOutOfRangeException("exceeded : max items : " + (singlearraymaxitems * arraysize).ToString());
            return _dictionaries[i].ElementAt((int)count);
        
        }
        public long Count { get
            {
                long count = 0;
                for (int i = 0; i < arraysize; i++)
                {
                    count += _dictionaries[i].Count;
                }
                return count;   
            }
        }

        public bool ContainsKey(TKey bytes)
        {
            bool bcontains = false;
            for (int i= 0; i < arraysize; i++)
            {
                if (_dictionaries[i].ContainsKey(bytes))
                {
                    bcontains = true;
                    break;
                }
            }
         
            return bcontains;
        }
        public TValue this[TKey key] { 
            get {
                for (int i = 0; i < arraysize; i++)
                {
                    if (_dictionaries[i].ContainsKey(key))
                    {
                        return _dictionaries[i][key];
                        
                    }
                }
                return default(TValue); 
            
            }    
            
            
            set {
                for (int i = 0; i < arraysize; i++)
                {
                    if (_dictionaries[i].Count < singlearraymaxitems)
                    {
                            _dictionaries[i][key] = value;
                            return;
                    }
                }
                throw new System.IndexOutOfRangeException("exceeded : max items : " + (singlearraymaxitems * arraysize).ToString());
               
            
            }    
        }

        public void Add(TKey vs, TValue count)
        {
            long c = Count;
            int i =  (int) (c/singlearraymaxitems);

            if (i>= arraysize || i==arraysize-1 && c%singlearraymaxitems== singlearraymaxitems-1) throw new System.IndexOutOfRangeException("exceeded : max items : " + (singlearraymaxitems * arraysize).ToString());

            _dictionaries[i].Add(vs, count);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < arraysize; i++)
            {
                foreach (KeyValuePair<TKey, TValue> p in _dictionaries[i])
                {
                    yield return p;
                }
            }
           
        }

       IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();

        }

  
    }
}
