#include "pch.h"
#include "Order.h"
#include "FileData.h"
#include "Item.h"

using namespace System;
using namespace System::Collections::Generic;

Order^ GetNewOrder()
{
    Order^ order = gcnew Order();

    Console::WriteLine("Создайте новую запись: ");

    try
    {
        Console::Write("Номер заказа: ");
        order->Number = int::Parse(Console::ReadLine());

        Console::Write("ФИО: ");
        order->FullName = Console::ReadLine();

        Console::Write("Товары (товар1 5,товар2 1, ...): ");
        String^ goodsStr = Console::ReadLine();

        array<String^>^ strings = goodsStr->Split(',');

        for (int i = 0; i < strings->Length; i++)
        {
            Item^ item;
            array<String^>^ arrStrs = strings[i]->Split(' ');

            if (arrStrs->Length == 2)
            {
                item = gcnew Item();
                item->Name = arrStrs[0];
                item->Count = int::Parse(arrStrs[1]);
                order->Goods->Add(item);
            }
            else
            {
                Console::WriteLine("Данные введены некорректно!");
                return nullptr;
            }
        }

        Console::Write("Цена: ");
        order->Amount = int::Parse(Console::ReadLine());
    }
    catch (FormatException^)
    {
        Console::WriteLine("Данные введены некорректно!");
        return nullptr;
    }

    return order;
}




int main(array<String^>^ args)
{
    Console::Title = "Лабораторная работа по АООД номер 4. Куликов В.Ю. (ИВБО-06-19)";
    Console::BackgroundColor = ConsoleColor::Black;
    Console::ForegroundColor = ConsoleColor::Green;


    List<Order^>^ orders = gcnew List<Order^>();
    FileData^ data = gcnew FileData();

    try
    {
        while (true)
        {
            String^ comand = Console::ReadLine();
            
            if (comand == "stop")
            {
                return 0;
            }
            else if (comand == "new")            
            {
                Order^ order = GetNewOrder();
                if (order == nullptr)
                    continue;
                else
                    orders->Add(order);
            }
            else if (comand == "save")
            {
                if (orders->Count != 0)
                {
                    Console::Write("Введите путь к фалу (формат xml): ");
                    String^ path = Console::ReadLine();

                    data->SaveData(path, orders);
                }
                else
                {
                    Console::Write("Список пуст!");
                }

            }
            else if (comand == "open") 
            {
                Console::Write("Введите путь к фалу (формат xml): ");
                String^ path = Console::ReadLine();

                List<Order^>^ openedOrders = data->GetData(path);

                if (openedOrders != nullptr && openedOrders->Count != 0)
                {
                    orders = openedOrders;

                    for (int i = 0; i < orders->Count; i++) {
                        String^ output = String::Format("Номер заказа: {0}\nФИО: {1}\nЦена: {2}",
                            orders[i]->Number, orders[i]->FullName, orders[i]->Amount);

                        Console::WriteLine(output);

                        for (int j = 0; j < orders[i]->Goods->Count; j++)
                        {
                            Item^ good = orders[i]->Goods[j];
                                Console::WriteLine(good->Name + " " + good->Count);
                        }
                    }

                }
                else
                {
                    Console::Write("Открытый список пуст!");
                }
            }
            else
            {
                Console::WriteLine("Неверная команда!");
            }
        }
    }
    catch (StackOverflowException^ ex)
    {
        Console::WriteLine(ex->Message);
        return 1;
    }

    return 0;
}