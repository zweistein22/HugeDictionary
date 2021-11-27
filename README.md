# HugeDictionary

C# Dictionaries have a limit of 2GB for all items . 
According to  see https://stackoverflow.com/questions/37578559/memory-usage-of-dictionaries-in-c-sharp  there is beween 12 and 20 bytes per dictionary record

and then the max Count of a dictionary reduces to 2GB/12bytes  , something between 100 and 200 i million.

To overcome this the HugeDictionary class was written.
