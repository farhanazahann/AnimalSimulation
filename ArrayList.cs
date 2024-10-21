using System;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class ArrayList<T>
{
    // The actual list
    private T[] array;
    private int count;

    // Constructor
    public ArrayList(int size)
    {
        array = new T[size + 1];
        count = 0;
    }

    private void Grow()
    {
        T[] newArray = new T[array.Length*2];
        for (int i = 0; i<array.Length; i++)
            newArray[i]=array[i];
        array = newArray;
        Console.WriteLine("Array size increased");
    }

    public void AddFront(T value)
    {
        if (GetCount() >= array.Length)
            Grow();

        for (int i = count; i > 0; i--)
        {
            array[i] = array[i-1];
        }
        array[0] = value;

        count++;
    }

    public void AddLast(T value)
    {
        if (GetCount() >= array.Length)
            Grow();

        array[count] = value;
        count++;
    }

    public int GetCount()
    {
        return count;
    }

    public void InsertBefore(T value, T value_before)
    {
        if (count >= array.Length)
            Grow();

        int position = 0;

        //  position = Array.IndexOf(array, value_before);
        //   OR
        for (int j = 0; j < count; j++)
        {
            if (array[j].Equals(value_before))
            {
                position = j;
            }
        }

        if (position != 0)
        {
            for (int i = count; i >= position; i--)
            {
                array[i] = array[i-1];
            }
            array[position] =  value;
        }
        else
        {
            AddFront(value);
        }

        count++;
    }

    public void InPlaceSort()
    {
        Array.Sort(array, 0, count);
    }

    public void Swap(int index1, int index2)
    {
        T temp = array[index1];
        array[index1] = array[index2];
        array[index2] = temp;
    }

    public void DeleteFirst()
    {
        array[0] = default;

        for (int i = 1; i < count; i++)
        {
            array[i-1] = array[i];
        }
        count--;
    }

    public void DeleteLast()
    {
        for (int i = count - 1; i >= 0; i--)
        {
            if (array[i] != null)
            {
                array[i] = default;
                count--;
                return;
            }
        }
    }

    public void RotateLeft()
    {
        if (count <= 1)
        {
            return;
        }

        T FirstElement = array[0];

        for (int i = 0; i < count; i++)
        {
            array[i] = array[i + 1];
        }

        array[count - 1] = FirstElement;
    }

    public void RotateRight()
    {
        if (count <= 1)
        {
            return;
        }

        T LastElement = array[count-1];

        for (int i = count; i > 0; i--)
        {
            array[i] = array[i-1];
        }

        array[0] = LastElement;
    }

    public ArrayList<T> Merge(ArrayList<T> List_1, ArrayList<T> List_2)
    {
        int l1 = List_1.GetCount();
        int l2 = List_2.GetCount();

        ArrayList<T> mergedList = new ArrayList<T>(l1+l2);

        for (int i = 0; i<l1; i++)
        {
            mergedList.AddLast(List_1.array[i]);
        }
        for (int j = 0; j<l2; j++)
        {
            mergedList.AddLast(List_2.array[j]);
        }

        return mergedList;
    }

    public String StringPrintAllForward()
    {
        string print = "";

        if (count!=0)
        {
            for (int i = 0; i < count; i++)
            {
                //  if(array[i]!=null)
                print += array[i].ToString() +"\n";
            }
        }
        else
        {
            print = "The array is empty : No entries made yet.";
        }

        return print;
    }

    public String StringPrintAllReverse()
    {
        string print = "";

        if (count!=0)
        {
            for (int i = count-1; i >=0; i--)
            {
                print = print + array[i].ToString() +"\n";
            }
        }
        else
        {
            print = "The array is empty : No entries made yet.";
        }

        return print;
    }

    public void Deleteall()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = default;
        }
        count = 0;
        //  Array.Clear(array);
    }

    public void DeleteItem(T item)
    {
        int value = -1;

        for (int i = 0; i < count; i++)
        {
            if (array[i].Equals(item))
            {
                value=i;
            }
        }
        if (value != -1)
        {
            for (int i = value; i < count; i++)
            {
                array[i] = array[i+1];
            }
            count--;
        }
    }

    public T At(int i)
    {
        T animal;
        animal = array[i];
        return animal;
    }



}