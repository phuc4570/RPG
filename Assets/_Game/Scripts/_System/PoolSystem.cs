using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Class để hỗ trợ tạo các Object Pooling
public class PoolSystem : SingletonObject<PoolSystem>
{
    //Dictionary để tìm kiếm các Pool đang có
    private Dictionary<int, PoolObject<Object>> dictionaryPool = new Dictionary<int, PoolObject<Object>>();
    //Dictionary để tìm kiếm object nào thuộc pool nào
    private Dictionary<Object, int> objectGroupDictionary = new Dictionary<Object, int>();

    //Yêu cầu một Object mới
    public Object RequiredObject(Object prefab)
    {
        //Lấy InstanceID của Object đó
        int id = prefab.GetInstanceID();
        //Kiểm tra có tồn tại Pool của Object đó chưa
        if (!dictionaryPool.ContainsKey(id))
        {
            //Nếu chưa, tạo một Pool mới
            var newPool = new PoolObject<Object>(prefab, transform);
            //Thêm Pool mới và dictionary
            dictionaryPool.Add(id, newPool);
        }
        //Yêu cầu một object mới từ Pool
        Object obj = dictionaryPool[id].RequiredObject();
        //Ghi nhận object đó thuộc pool gì
        if (!objectGroupDictionary.ContainsKey(obj))
        {
            objectGroupDictionary.Add(obj, id);
        }
        return obj;
    }

    //Trà object về pool
    public void ReturnObject(Object obj)
    {
        //Trả object đó về đúng pool
        if (objectGroupDictionary.ContainsKey(obj))
        {
            dictionaryPool[objectGroupDictionary[obj]].ReturnObject(obj);
        }
    }
}

//Class quản lý Pool
public class PoolObject<T> where T : Object
{
    //Prefab để tạo mới Object
    public T prefab;
    //Transform chứa các Object
    public Transform container;
    //Danh sách các Object
    public List<T> list = new List<T>();

    //Hàm khởi tạo
    public PoolObject(T prefab, Transform parent)
    {
        GameObject newContainer = new GameObject($"{prefab.name}");
        newContainer.transform.parent = parent;
        container = newContainer.transform;
        this.prefab = prefab;
    }

    //Yêu cầu một Object mới
    public T RequiredObject()
    {
        T newObj = null;

        //Nếu list Object có nhiều hơn 1 Object
        if (list.Count > 0)
        {
            //Lấy ra Object đang bị disable
            newObj = list.Find(e => e && !e.GameObject().activeSelf);
        }

        //Nếu không có Object có sẵn nào
        if (!newObj)
        {
            //Tạo mới Object
            newObj = Object.Instantiate(prefab, container, false);
            list.Add(newObj);
        }

        //Hiện object lên
        newObj.GameObject().SetActive(true);

        return newObj;
    }

    //Trả lại 1 object về pool
    public void ReturnObject(T obj)
    {
        GameObject GObj = obj.GameObject();
        //Set vị trí của object thành object con của container
        GObj.transform.SetParent(container, false);
        //Ẩn object đi
        GObj.SetActive(false);
    }
}
