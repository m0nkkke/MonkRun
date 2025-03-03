using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataProvider
{
    void Save();

    bool TryLoad();
}
