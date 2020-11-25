#include "pch.h"

using namespace System;
using namespace System::Text::RegularExpressions;

int main(array<String^>^ args)
{
    Console::Title = "Лабораторная работа по АООД номер 3. Куликов В.Ю. (ИВБО-06-19)";
    Console::BackgroundColor = ConsoleColor::Black;
    Console::ForegroundColor = ConsoleColor::Green;

    Console::Write("Введите адрес электронной почты для проверки: ");

    String^ address = Console::ReadLine();
    int aPosition = address->IndexOf('@');

    Regex^ lettersRegex = gcnew Regex("[a-zа-я]", RegexOptions::IgnoreCase | RegexOptions::Compiled);
    Regex^ allRegex = gcnew Regex("[a-zа-я0-9]", RegexOptions::IgnoreCase | RegexOptions::Compiled);


    if (aPosition == -1)
    {
        Console::WriteLine("Введенная строка не является адресом электронной почты!");
        return 1;
    }
    else
    {
        if (lettersRegex->IsMatch(address[0].ToString()) && allRegex->IsMatch(address[aPosition - 1].ToString()))
        {
            Console::WriteLine("Соответствует!");
        }
        else
        {
            Console::WriteLine("Не соответствует!");
        }
    }


    Console::ReadKey();
    return 0;
}
