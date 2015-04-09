# Unity Helpers

A collection of simple unity script that might be useful in all kinds of projects.

## Most Notable

### SerializableDictionary

This is a very simple class that makes it possible to use dictionaries and have unity serialize them. Its a dictionary itself, so works exactly like the real one, but adds some stuff to make the serialization possible.

Usage is a little strange though, for some reason you can't use it directly in unity. You have to make a non-generic instance of it, and then use that. This is luckily quite easy:

    [System.Serializable]
    class MyDictionary : SerializableDictionary<KeyType, ValueType> {}

Then make an instance of this like this:

    [SerializeField]
    private MyDictionary dictionary = new MyDictionary();

Now you can use it as if it were a normal dictionary.

### AssetCreator

This class adds an option in the Assets/Create menu. The option is called "From Selected". Using it creates an instance of the selected `ScriptableObject`. Normally you have to create a method for each asset you want to create, but with this that's no longer needed.

### AssetsHelper

This class contains only a single method (or at least now it does, might be more in the future).

    IEnumerable<T> GetAllAssetsOfType<T>();

This method returns all assets of the given type, even when they aren't loaded into memory.

## Further developer and suggestions

I'll continue adding interesting code when I need it myself. I'm always interested in hearing improvements or suggestions. Just add an issue and I'll take a look at it. The same, of course, goes for bugs you find.

# License

You can use this code in any of your unity projects and modify it to your liking. However you can't sell it without permission, even if you modified it, unless it's in a unity build. I can't promise the code will work as you expect it to, nor that it won't damage stuff. Anything else, ask.
