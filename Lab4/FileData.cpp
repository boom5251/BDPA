#include "pch.h"
#include "FileData.h"
#include "Order.h"

FileData::FileData()
{
	List<Order^>^ orders = gcnew List<Order^>(0);
	Type^ type = ((Object^)orders)->GetType();
	Serializer = gcnew XmlSerializer(type);
}


void FileData::SaveData(String^ path, List<Order^>^ orders)
{
	try
	{
		FileStream^ fs = gcnew FileStream(path, FileMode::OpenOrCreate);
		Serializer->Serialize(fs, orders);
		fs->Close();
	}
	catch (IOException^ ex)
	{
		Console::WriteLine(ex->Message);
	}
}


List<Order^>^ FileData::GetData(String^ path)
{
	try
	{
		FileStream^ fs = gcnew FileStream(path, FileMode::Open);
		Object^ obj = Serializer->Deserialize(fs);
		fs->Close();
		return (List<Order^>^)obj;
	}
	catch (IOException^ ex)
	{
		Console::WriteLine(ex->Message);
	}
}