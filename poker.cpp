#include <locale.h>
#include <stdlib.h>
#include <stdio.h>
#include <iostream>
#include <cmath>
#include <ctime>
#include "io.h"
#include "fcntl.h"
using namespace std;
bool maingo; //Игра идет
class pok
{
private:
    int c1[10]; //Первая карта игрока 
    int c2[10]; //Вторая карта игрока
    int cstol[100]; //Карты на столе
    bool deadcard[53]; //Выпавшие карты
    static int rate[3]; //Ставка игрока

public:
    static int chip[3]; //Фишки игрока
    bool go; //Раунд идет
    static int raund; //Номер раунда
    int strong[3]; //Сила комбинации у игрока
    //Обнуление силы и массива выпавших карт, раздача карт на стол
    pok(){
        for(int i=0; i<=52; i++)
        deadcard[i] = false;
        strong[1]=0;
        strong[2]=0;
        for(int i=0; i<5; i++){
            do
        cstol[i] = rand() % 52 + 1; 
        while (deadcard[cstol[i]] == true);
        deadcard[cstol[i]] = true;
        }
        go=true;
        maingo=true;
        raund++;
        if(raund==1){
            chip[1]=400;
            chip[2]=400;
        }
    }
    //Раздает карты игроку id
    void razd_player(int id)
    {
        system("cls");
        do
        c1[id] = rand() % 52 + 1; 
        while (deadcard[c1[id]] == true);
        deadcard[c1[id]] = true;
        do
        c2[id] = rand() % 52 + 1; 
        while (deadcard[c2[id]] == true);
        deadcard[c2[id]] = true;
        show_player(id);
        cout << endl;
        system("pause");
        system("cls");
    }
    //Показывает карту по номеру
    void card_show(int n)
    {
        if (n%13==1)
            cout << "2";
        if (n%13==2)
            cout << "3";
        if (n%13==3)
            cout << "4";
        if (n%13==4)
            cout << "5";
        if (n%13==5)
            cout << "6";
        if (n%13==6)
            cout << "7";
        if (n%13==7)
            cout << "8";
        if (n%13==8)
            cout << "9";
        if (n%13==9)
            cout << "10";
        if (n%13==10)
            cout << "В";
        if (n%13==11)
            cout << "Д";
        if (n%13==12)
            cout << "К";
        if (n%13==0)
            cout << "Т";
        if (n<=13){
        _setmode(_fileno(stdout), _O_U16TEXT);
        wcout << L" \x2660"; //пики
        _setmode(_fileno(stdout), _O_TEXT);
        }
        if ((n>=14)&&(n<=26)){
        _setmode(_fileno(stdout), _O_U16TEXT);
        wcout << L" \x2665"; //черви
        _setmode(_fileno(stdout), _O_TEXT);
        }
        if ((n>=27)&&(n<=39)){
        _setmode(_fileno(stdout), _O_U16TEXT);
        wcout << L" \x2666"; //буби
        _setmode(_fileno(stdout), _O_TEXT);
        }
        if ((n>=40)&&(n<=52)){
        _setmode(_fileno(stdout), _O_U16TEXT);
        wcout << L" \x2663"; //треф
        _setmode(_fileno(stdout), _O_TEXT);
        }
    }
    //Показывает карты на столе
    void show_stol(int k){
        cout << "Стол \n";
        for (int i=0; i<k; i++){
        card_show(cstol[i]);
        cout << "   ";
        }
        cout << endl;
        //show_player(1);
        //cout << "           ";
        //show_player(2);
        cout << endl;
        ///system("pause");
    }
    //Показывает карты которые уже выпали
    void show_dead(){
        for(int i=0; i<=52; i++)
        if (deadcard[i] == true)
            cout << i << endl;

    }
    //Показывает карты на руках игрока id
    void show_player(int id){
       card_show(c1[id]);
        cout <<"    ";
        card_show(c2[id]); 
    }
    //Проверка комбинаций
    void chek(int id){
        for(int i=0; i<5; i++){
            for(int j=0; j<13; j++){
                int cn=1+j;
                int cnp =1+j;
                if (cn==13)
                    cn=0;
                if(((c1[id])%13==cn)&&((c2[id])%13==cn)||((cstol[i])%13==cn)&&(((c2[id])%13==cn)||((c1[id])%13==cn))) //Пара
                {
                    if (strong[id]<=(cnp))
                        strong[id]=cnp;
                }
                
                if(((cstol[i])%13==cn)&&(((c2[id])%13==cn)||((c1[id])%13==cn))) //Две пары
                {
                    for(int i2=0; i2<5; i2++){
                        for(int i3=0; i3<5; i3++)
                        for(int j2=0; j2<13; j2++){
                            int cn2=1+j2;
                            int cnp2 =1+j2;
                            if (cn2==13)
                                cn2=0;
                            if((i2==i))
                                i2++;
                            if((i3==i)||(i3==i2))
                                i3++;
                            if((i3==i)||(i3==i2))
                                i3++;
                            if((((cstol[i2])%13==cn2)&&(((c2[id])%13==cn2)||((c1[id])%13==cn2)))||(((cstol[i3])%13==cn2)&&((cstol[i2])%13==cn2))){
                                if (strong[id]<=(20+cnp2))
                                    strong[id]=20+cnp2;
                                if (strong[id]<=(20+cnp))
                                    strong[id]=20+cnp;
                            }
                        }
                    }
                }
                if(((c1[id])%13==cn)&&((c2[id])%13==cn)) //Две пары
                {
                    for(int i2=0; i2<5; i2++){
                        for(int j2=0; j2<13; j2++){
                            int cn2=1+j2;
                            int cnp2 =1+j2;
                            if (cn2==13)
                                cn2=0;
                            if(i2==i)
                                i2++;
                            if(((cstol[i])%13==cn2)&&((cstol[i2])%13==cn2)){
                                if (strong[id]<=(20+cnp))
                                    strong[id]=20+cnp;
                            }
                        }
                    }
                }
                
                if((((c1[id])%13==cn)||((c2[id])%13==cn))&&((cstol[i])%13==cn)) //Сет
                {
                    for(int i2=i+1; i2<5; i2++){
                        if(((cstol[i])%13==cn)&&((cstol[i2])%13==cn))
                            if (strong[id]<=(40+cnp))
                                strong[id]=40+cnp;
                    }
                }
                if(((c1[id])%13==cn)&&((c2[id])%13==cn)&&((cstol[i])%13==cn)) //Сет
                {
                    if (strong[id]<=(40+cnp))
                        strong[id]=40+cnp;
                }
                
                if((((c1[id])%13==cn)||((c2[id])%13==cn))&&((cstol[i])%13==cn)) //Каре
                {
                    for(int i2=i+1; i2<5; i2++){
                        if(((cstol[i])%13==cn)&&((cstol[i2])%13==cn))
                            for(int i3=i2+1; i3<5; i3++){
                                if(((cstol[i3])%13==cn)&&((cstol[i2])%13==cn))
                                    if (strong[id]<=(120+cnp))
                                        strong[id]=120+cnp;
                            }
                    }
                }
                if(((c1[id])%13==cn)&&((c2[id])%13==cn)&&((cstol[i])%13==cn)) //Каре
                {
                    for(int i2=i+1; i2<5; i2++){
                        if(((cstol[i])%13==cn)&&((cstol[i2])%13==cn))
                            if (strong[id]<=(120+cnp))
                                strong[id]=120+cnp;
                    }
                }
                
                if(((cstol[i])%13==cn)&&(((c2[id])%13==cn)||((c1[id])%13==cn))) //Фуллхауз
                {
                    for(int i2=0; i2<5; i2++){
                        for(int i3=0; i3<5; i3++)
                        for(int i4=0; i4<5; i4++)
                        for(int j2=0; j2<13; j2++){
                            int cn2=1+j2;
                            int cnp2 =1+j2;
                            if (cn2==13)
                                cn2=0;
                            if((i2==i))
                                i2++;
                            if((i3==i)||(i3==i2))
                                i3++;
                            if((i3==i)||(i3==i2))
                                i3++;
                            if((i4==i)||(i4==i2)||(i4==i3))
                                i4++;
                            if((i4==i)||(i4==i2)||(i4==i3))
                                i4++;
                            if((i4==i)||(i4==i2)||(i4==i3))
                                i4++;
                            if((i4>=5)||(i3>=5)||(i2>=5))
                                break;
                            if(((((cstol[i2])%13==cn2)&&(((c2[id])%13==cn2)||((c1[id])%13==cn2)))||(((cstol[i3])%13==cn2)&&((cstol[i2])%13==cn2)))&&((cstol[i4])%13==cn)){
                                if (strong[id]<=(100+cnp))
                                    strong[id]=100+cnp;
                            }
                            if(((((cstol[i2])%13==cn2)&&(((c2[id])%13==cn2)||((c1[id])%13==cn2)))||(((cstol[i3])%13==cn2)&&((cstol[i2])%13==cn2)))&&((cstol[i4])%13==cn2)){
                                if (strong[id]<=(100+cnp2))
                                    strong[id]=100+cnp2;
                            }
                        }
                    }
                }
                if(((c1[id])%13==cn)&&((c2[id])%13==cn)) //Фуллхауз
                {
                    for(int i2=0; i2<5; i2++){
                        for(int i3=0; i3<5; i3++)
                        for(int j2=0; j2<13; j2++){
                            int cn2=1+j2;
                            int cnp2 =1+j2;
                            if (cn2==13)
                                cn2=0;
                            if(i2==i)
                                i2++;
                            if((i3==i)||(i3==i2))
                                i3++;
                            if((i3==i)||(i3==i2))
                                i3++;
                            if(((cstol[i])%13==cn)&&((cstol[i3])%13==cn2)&&((cstol[i2])%13==cn2)){
                                if (strong[id]<=(100+cnp))
                                    strong[id]=100+cnp;
                            }
                            if(((cstol[i])%13==cn2)&&((cstol[i3])%13==cn2)&&((cstol[i2])%13==cn2)){
                                if (strong[id]<=(100+cnp2))
                                    strong[id]=100+cnp2;
                            }
                        }
                    }
                }
                
                if((((c1[id]-1)/13)==((cstol[i]-1)/13))||(((c2[id]-1)/13)==((cstol[i]-1)/13))){ //Флеш
                    for(int i2=i+1; i2<5; i2++)
                        if(((cstol[i2]-1)/13)==((cstol[i]-1)/13))
                            for(int i3=i2+1; i3<5; i3++)
                                if(((cstol[i2]-1)/13)==((cstol[i3]-1)/13))
                                    for(int i4=i3+1; i4<5; i4++){
                                        if(((cstol[i4]-1)/13)==((cstol[i3]-1)/13)){
                                            int cnp2=cstol[i]%13;
                                            if (cnp2<(cstol[i2]%13))
                                                cnp2=cstol[i2]%13;
                                            if (cnp2<(cstol[i3]%13))
                                                cnp2=cstol[i3]%13;
                                            if (cnp2<(cstol[i4]%13))
                                                cnp2=cstol[i4]%13;
                                            if(((c1[id]-1)/13)==((cstol[i3]-1)/13)){
                                                if (cnp2<(c1[id]%13))
                                                    cnp2=c1[id]%13;
                                                if (((c1[id]%13)==0)||((cstol[i4]%13)==0)||((cstol[i3]%13)==0)||((cstol[i2]%13)==0)||((cstol[i]%13)==0))
                                                    cnp2=13;
                                            }
                                            if(((c2[id]-1)/13)==((cstol[i3]-1)/13)){
                                                if (cnp2<(c2[id]%13))
                                                    cnp2=c2[id]%13;
                                                if (((c2[id]%13)==0)||((cstol[i4]%13)==0)||((cstol[i3]%13)==0)||((cstol[i2]%13)==0)||((cstol[i]%13)==0))
                                                    cnp2=13;
                                            }
                                            if (strong[id]<=(80+cnp2))
                                                strong[id]=80+cnp2;
                                        }
                                        if(((c1[id]-1)/13)==((c2[id]-1)/13)){
                                            int cnp2=cstol[i]%13;
                                            if (cnp2<(cstol[i2]%13))
                                                cnp2=cstol[i2]%13;
                                            if (cnp2<(cstol[i3]%13))
                                                cnp2=cstol[i3]%13;
                                            if (cnp2<(c1[id]%13))
                                                cnp2=c1[id]%13;
                                            if (cnp2<(c2[id]%13))
                                                cnp2=c2[id]%13;
                                            if (((c1[id]%13)==0)||((c2[id]%13)==0)||((cstol[i3]%13)==0)||((cstol[i2]%13)==0)||((cstol[i]%13)==0))
                                                cnp2=13;
                                            if (strong[id]<=(80+cnp2))
                                                strong[id]=80+cnp2;
                                        }
                                    }                                    
                }
                
                if(1==1){//Стрит
                    int min=cstol[i]%13;
                    for(int i2=0; i2<5; i2++)
                        if(((cstol[i2]%13==min+1)||(c2[id]%13==min+1)||(c1[id]%13==min+1)))
                            for(int i3=0; i3<5; i3++)
                                if(((cstol[i3]%13==min+2)||(c2[id]%13==min+2)||(c1[id]%13==min+2)))
                                    for(int i4=0; i4<5; i4++)
                                        if(((cstol[i4]%13==min+3)||(c2[id]%13==min+3)||(c1[id]%13==min+3)))
                                            for(int i5=0; i5<5; i5++){
                                                if(min+4==13)
                                                    if(((cstol[i5]%13==0)||(c2[id]%13==0)||(c1[id]%13==0)))
                                                        for(int i6=0; i6<5; i6++)
                                                            if((c2[id]%13==min+i6)||(c1[id]%13==min+i6)||(c2[id]%13==0)||(c1[id]%13==0))
                                                                if (strong[id]<=(60+min+4))
                                                                    strong[id]=60+min+4;
                                                if(((cstol[i5]%13==min+4)||(c2[id]%13==min+4)||(c1[id]%13==min+4)))
                                                    for(int i6=0; i6<5; i6++)
                                                        if((c2[id]%13==min+i6)||(c1[id]%13==min+i6))
                                                            if (strong[id]<=(60+min+4))
                                                                strong[id]=60+min+4;
                                            }
                    min=c1[id]%13;
                    for(int i2=0; i2<5; i2++)
                        if(((cstol[i2]%13==min+1)||(c2[id]%13==min+1)))
                            for(int i3=0; i3<5; i3++)
                                if(((cstol[i3]%13==min+2)||(c2[id]%13==min+2)))
                                    for(int i4=0; i4<5; i4++)
                                        if(((cstol[i4]%13==min+3)||(c2[id]%13==min+3)))
                                            for(int i5=0; i5<5; i5++){
                                                if(min+4==13)
                                                    if(((cstol[i5]%13==0)||(c2[id]%13==0)))
                                                        if (strong[id]<=(60+min+4))
                                                            strong[id]=60+min+4;
                                                if(((cstol[i5]%13==min+4)||(c2[id]%13==min+4)))
                                                    if (strong[id]<=(60+min+4))
                                                        strong[id]=60+min+4;
                                            }
                    min=c2[id]%13;
                    for(int i2=0; i2<5; i2++)
                        if(((cstol[i2]%13==min+1)||(c1[id]%13==min+1)))
                            for(int i3=0; i3<5; i3++)
                                if(((cstol[i3]%13==min+2)||(c1[id]%13==min+2)))
                                    for(int i4=0; i4<5; i4++)
                                        if(((cstol[i4]%13==min+3)||(c1[id]%13==min+3)))
                                            for(int i5=0; i5<5; i5++){
                                                if(min+4==13)
                                                    if(((cstol[i5]%13==0)||(c1[id]%13==0)))
                                                        if (strong[id]<=(60+min+4))
                                                            strong[id]=60+min+4;
                                                if(((cstol[i5]%13==min+4)||(c1[id]%13==min+4)))
                                                    if (strong[id]<=(60+min+4))
                                                        strong[id]=60+min+4;
                                            }
                }

                if(1==1){//Стрит флеш
                    int min=cstol[i]%13;
                    int mast=(cstol[i]-1)/13;
                    for(int i2=0; i2<5; i2++)
                        if(((cstol[i2]%13==min+1)&&((cstol[i2]-1)/13==mast))||((c2[id]%13==min+1)&&((c2[id]-1)/13==mast))||((c1[id]%13==min+1)&&((c1[id]-1)/13==mast)))
                            for(int i3=0; i3<5; i3++)
                                if(((cstol[i3]%13==min+2)&&((cstol[i3]-1)/13==mast))||((c2[id]%13==min+2)&&((c2[id]-1)/13==mast))||((c1[id]%13==min+2)&&((c1[id]-1)/13==mast)))
                                    for(int i4=0; i4<5; i4++)
                                        if(((cstol[i4]%13==min+3)&&((cstol[i4]-1)/13==mast))||((c2[id]%13==min+3)&&((c2[id]-1)/13==mast))||((c1[id]%13==min+3)&&((c1[id]-1)/13==mast)))
                                            for(int i5=0; i5<5; i5++){
                                                if(min+4==13)
                                                if(((cstol[i5]%13==0)&&((cstol[i5]-1)/13==mast))||((c2[id]%13==0)&&((c2[id]-1)/13==mast))||((c1[id]%13==0)&&((c1[id]-1)/13==mast)))
                                                    for(int i6=0; i6<5; i6++)
                                                        if((c2[id]%13==min+i6)||(c1[id]%13==min+i6)||(c2[id]%13==0)||(c1[id]%13==0))
                                                                if (strong[id]<=(140+min+4))
                                                                strong[id]=140+min+4;
                                                if(((cstol[i5]%13==min+4)&&((cstol[i5]-1)/13==mast))||((c2[id]%13==min+4)&&((c2[id]-1)/13==mast))||((c1[id]%13==min+4)&&((c1[id]-1)/13==mast)))
                                                    for(int i6=0; i6<5; i6++)
                                                        if((c2[id]%13==min+i6)||(c1[id]%13==min+i6))
                                                                if (strong[id]<=(140+min+4))
                                                                strong[id]=140+min+4;
                                                                }
                    min=c1[id]%13;
                    if(((c1[id]-1)/13==mast))
                        for(int i2=0; i2<5; i2++)
                            if(((cstol[i2]%13==min+1)&&((cstol[i2]-1)/13==mast))||((c2[id]%13==min+1)&&((c2[id]-1)/13==mast)))
                                for(int i3=0; i3<5; i3++)
                                    if(((cstol[i3]%13==min+2)&&((cstol[i3]-1)/13==mast))||((c2[id]%13==min+2)&&((c2[id]-1)/13==mast)))
                                        for(int i4=0; i4<5; i4++)
                                            if(((cstol[i4]%13==min+3)&&((cstol[i4]-1)/13==mast))||((c2[id]%13==min+3)&&((c2[id]-1)/13==mast)))
                                                for(int i5=0; i5<5; i5++){
                                                if(min+4==13)
                                                    if(((cstol[i5]%13==0)&&((cstol[i5]-1)/13==mast))||((c2[id]%13==0)&&((c2[id]-1)/13==mast)))
                                                        if (strong[id]<=(140+min+4))
                                                            strong[id]=140+min+4;
                                                    if(((cstol[i5]%13==min+4)&&((cstol[i5]-1)/13==mast))||((c2[id]%13==min+4)&&((c2[id]-1)/13==mast)))
                                                        if (strong[id]<=(140+min+4))
                                                            strong[id]=140+min+4;
                                                }
                    min=c2[id]%13;
                    if(((c2[id]-1)/13==mast))
                        for(int i2=0; i2<5; i2++)
                            if(((cstol[i2]%13==min+1)&&((cstol[i2]-1)/13==mast))||((c1[id]%13==min+1)&&((c1[id]-1)/13==mast)))
                                for(int i3=0; i3<5; i3++)
                                    if(((cstol[i3]%13==min+2)&&((cstol[i3]-1)/13==mast))||((c1[id]%13==min+2)&&((c1[id]-1)/13==mast)))
                                        for(int i4=0; i4<5; i4++)
                                            if(((cstol[i4]%13==min+3)&&((cstol[i4]-1)/13==mast))||((c1[id]%13==min+3)&&((c1[id]-1)/13==mast)))
                                                for(int i5=0; i5<5; i5++){
                                                if(min+4==13)
                                                    if(((cstol[i5]%13==0)&&((cstol[i5]-1)/13==mast))||((c1[id]%13==0)&&((c1[id]-1)/13==mast)))
                                                        if (strong[id]<=(140+min+4))
                                                            strong[id]=140+min+4;
                                                    if(((cstol[i5]%13==min+4)&&((cstol[i5]-1)/13==mast))||((c1[id]%13==min+4)&&((c1[id]-1)/13==mast)))
                                                        if (strong[id]<=(140+min+4))
                                                            strong[id]=140+min+4;
                                                }
                }
                
                if(true){//Флеш рояль
                    int min=cstol[i]%13;
                    int mast=(cstol[i]-1)/13;
                    if(min==9)
                        for(int i2=0; i2<5; i2++)
                            if(((cstol[i2]%13==min+1)&&((cstol[i2]-1)/13==mast))||((c2[id]%13==min+1)&&((c2[id]-1)/13==mast))||((c1[id]%13==min+1)&&((c1[id]-1)/13==mast)))
                                for(int i3=0; i3<5; i3++)
                                    if(((cstol[i3]%13==min+2)&&((cstol[i3]-1)/13==mast))||((c2[id]%13==min+2)&&((c2[id]-1)/13==mast))||((c1[id]%13==min+2)&&((c1[id]-1)/13==mast)))
                                        for(int i4=0; i4<5; i4++)
                                            if(((cstol[i4]%13==min+3)&&((cstol[i4]-1)/13==mast))||((c2[id]%13==min+3)&&((c2[id]-1)/13==mast))||((c1[id]%13==min+3)&&((c1[id]-1)/13==mast)))
                                                for(int i5=0; i5<5; i5++)
                                                    if(((cstol[i5]%13==0)&&((cstol[i5]-1)/13==mast))||((c2[id]%13==0)&&((c2[id]-1)/13==mast))||((c1[id]%13==0)&&((c1[id]-1)/13==mast)))
                                                        for(int i6=0; i6<5; i6++)
                                                            if((c2[id]%13==min+i6)||((c1[id]%13==min+i6)))
                                                                    if (strong[id]<=(160+min+4))
                                                                    strong[id]=160+min+4;
                    min=c1[id]%13;
                    mast=(c1[id]-1)/13;
                    if(min==9)
                        if(((c1[id]-1)/13==mast))
                            for(int i2=0; i2<5; i2++)
                                if(((cstol[i2]%13==min+1)&&((cstol[i2]-1)/13==mast))||((c2[id]%13==min+1)&&((c2[id]-1)/13==mast)))
                                    for(int i3=0; i3<5; i3++)
                                        if(((cstol[i3]%13==min+2)&&((cstol[i3]-1)/13==mast))||((c2[id]%13==min+2)&&((c2[id]-1)/13==mast)))
                                            for(int i4=0; i4<5; i4++)
                                                if(((cstol[i4]%13==min+3)&&((cstol[i4]-1)/13==mast))||((c2[id]%13==min+3)&&((c2[id]-1)/13==mast)))
                                                    for(int i5=0; i5<5; i5++)
                                                        if(((cstol[i5]%13==0)&&((cstol[i5]-1)/13==mast))||((c2[id]%13==0)&&((c2[id]-1)/13==mast)))
                                                            if (strong[id]<=(160+min+4))
                                                                strong[id]=160+min+4;
                    min=c2[id]%13;
                    mast=(c2[id]-1)/13;
                    if(min==9)
                        if(((c2[id]-1)/13==mast))
                            for(int i2=0; i2<5; i2++)
                                if(((cstol[i2]%13==min+1)&&((cstol[i2]-1)/13==mast))||((c1[id]%13==min+1)&&((c1[id]-1)/13==mast)))
                                    for(int i3=0; i3<5; i3++)
                                        if(((cstol[i3]%13==min+2)&&((cstol[i3]-1)/13==mast))||((c1[id]%13==min+2)&&((c1[id]-1)/13==mast)))
                                            for(int i4=0; i4<5; i4++)
                                                if(((cstol[i4]%13==min+3)&&((cstol[i4]-1)/13==mast))||((c1[id]%13==min+3)&&((c1[id]-1)/13==mast)))
                                                    for(int i5=0; i5<5; i5++)
                                                        if(((cstol[i5]%13==0)&&((cstol[i5]-1)/13==mast))||((c1[id]%13==0)&&((c1[id]-1)/13==mast)))
                                                            if (strong[id]<=(160+min+4))
                                                                strong[id]=160+min+4;
                }
            }
        }
    }
    //Ручная установка карт
    void set_card(){
        for(int i=0; i<5; i++)
        cin>>cstol[i]; 
        cin>>c1[1]; 
        cin>>c2[1]; 
        cin>>c1[2]; 
        cin>>c2[2];
    }
    //Ход игроков
    void motion(int i){
        int com, j2, j, x;
        bool y=false, z=false;
        if(raund%2==1){
            if(rate[1]==0){
                rate[1]=5;
                rate[2]=10;
            }
            j2=2;
            j=1;
        }
        else{
            if(rate[1]==0){
                rate[1]=10;
                rate[2]=5;
            }
            j2=1;
            j=2;
        }
        do{
            cout << "Текущие ставки"<<endl<<"Игрок "<<j<<": "<<rate[j]<<"           Игрок "<<j2<<": "<<rate[j2]<<endl;
            cout << "Игрок "<<j<<": Чек/Колл(1); Рейз(2); Фолд(3); Показать карты(4)"<<endl;
            cin >> com;
            if(com==1){
                if(rate[j2]>rate[j])
                    rate[j]=rate[j2];
                if(y==true)
                    z=true;
                x=j2;
                j2=j;
                j=x;
                y=true;
            }
            else if(com==2){
                cout << "Баланс игрока 1: "<<chip[1]<<" Баланс игрока 2: "<<chip[2]<<endl;
                int r;
                cin>>r;
                if((rate[j]+r)>(chip[j]))
                    r=chip[j]-rate[j];
                rate[j]+=r;
                x=j2;
                j2=j;
                j=x;
                y=true;
            }
            else if(com==4){
                show_player(j);
                system("pause");
                system("cls");
                show_stol(i);
            }
            else{
                strong[j2]=200;
                end_raund();
                break;
            }
        }
        while (((rate[1]!=rate[2])||(rate[1]==0)||(rate[2]==0))||(z==false));
        system("cls");
    }
    //Завершение раунда
    void end_raund(){
        if(strong[1]>strong[2]){
            chip[1]+=rate[2];
            chip[2]-=rate[2];
            cout<<endl<<"Игрок 1 выиграл"<<endl;
            }
        if(strong[2]>strong[1]){
            chip[2]+=rate[1];
            chip[1]-=rate[1];
            cout<<endl<<"Игрок 2 выиграл"<<endl;
        }
        if(strong[2]==strong[1])
            cout<<endl<<"Ничья"<<endl;
        rate[1]=0;
        rate[2]=0;
        go=false;
        if((chip[1]==0)||(chip[2]==0))
            maingo=false;
        system("pause");
    }
};
int pok :: chip[3];
int pok :: rate[3];
int pok :: raund;

int main()
{
    setlocale(LC_ALL, "Russian");
    cout<<"Игра началась \n";
    system("pause");
    maingo = true;
    while (maingo == true)
    {
        srand(time(NULL));
        system("cls");
        pok game;
        cout << "Баланс игрока 1: "<<game.chip[1]<<" Баланс игрока 2: "<<game.chip[2]<<endl;
        system("pause");
        system("cls");
        game.razd_player(1);
        game.razd_player(2);
        game.motion(0);
        if (game.go == true){
            game.show_stol(3);
            game.motion(3);
            if (game.go == true){
                game.show_stol(4);
                game.motion(4);
                if (game.go == true){
                    game.show_stol(5);
                    game.motion(5);
                    if (game.go == true){
                        game.chek(1);
                        game.chek(2);
                        //cout << game.strong[1]<<"                       "<< game.strong[2]<< endl;
                        //if(((game.strong[1]>=160)&&(game.strong[1]<180))||((game.strong[2]>=160))&&(game.strong[2]<180))
                        system("pause");
                        game.end_raund();
                    }
                }
            }
        }
    }
    while (maingo == true);
    cout << endl;
    return 0;
}