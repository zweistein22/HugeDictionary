# HugeDictionary

C# Dictionaries have a limit of 2GB for all items . 
According to  https://stackoverflow.com/questions/37578559/memory-usage-of-dictionaries-in-c-sharp  data usage is at least  12 and 20 bytes per dictionary record

thus the max Count of a dictionary reduces to 2GB/20Bytess  , something around 100 Million records (= dictionary.Count).

To overcome this the HugeDictionary class was written.
