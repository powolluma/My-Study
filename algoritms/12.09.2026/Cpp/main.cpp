#include <iostream>
#include <cmath>
using namespace std;

// Делил действия примеров, мне так удобнее решать
int main(){
    setlocale(LC_ALL, "ru");

    // 1
    /*
    double x = 14.26;
    double y = -1.22;
    double z = 3.5 * pow(10, -2);
    double t_1 = 0.564849;

    double a_1 = 2.0 * (cos(x - (M_PI / 6)));
    double a_2 = 0.5 + pow(sin(y), 2.0);
    double a_3 = 1.0 + ((pow(z, 2) / (3.0 - pow(z,2) / 5.0)));
    double a_4 = (a_1 / a_2) * a_3;

    cout << t_1 << endl;
    cout << a_4;
    */

    // 2
    /*
    double x = -4.5;
    double y = 0.75 * pow(10, -4.0);
    double z = 0.845 * pow(10, 2.0);
    double u = -55.6848;

    double a_1 = pow((8.0 + pow(fabs(x - y), 2.0) + 1.0), 1.0/3.0);
    double a_2 = pow(x,2.0) + pow(y,2.0) + 2.0;
    double a_3 = exp(fabs(x-y));
    double a_4 = pow(pow(tan(z), 2.0) + 1, x);
    double a_5 = (a_1 / a_2) - (a_3 * a_4);
    
    cout << u << endl;
    cout << a_5;
    */

    // 3
    /*
    double x = 3.74 * pow(10, -2);
    double y = -0.825;
    double z = 0.16 * pow(10,2);
    double v = 1.0553;

    double a_1 = 1 + pow((sin(x+y)), 2);
    double a_2 = fabs(x - ((2 * y) / 1 + pow(x, 2) * pow(y,2)));
    double a_3 = (a_1 / a_2) * (pow(x, abs(y)));
    double a_4 = a_3 + pow((cos(atan(1/z))), 2);

    cout << v << endl;
    cout << a_4;
    */

    // 4
    /*
    double x = 0.4 * pow(10.0,4.0);
    double y = -0.875;
    double z = -0.475 * pow(10.0,-3.0);
    double w = 1.9873;

    double a_1 = pow(fabs(cos(x) - cos(y)), 1.0 + pow(sin(y), 2.0) * 2.0);
    double a_2 = 1.0 + z + (pow(z,2.0) / 2.0) + (pow(z,3.0) / 3.0) + (pow(z,4.0) / 4.0);
    double a_3 = a_1 * a_2;

    cout << w << endl;
    cout << a_3;
    */

    // 5
    /*
    double x = -15.246;
    double y = 4.642 * pow(10.0, -2.0);
    double z = 20.001 * pow(10.0,2.0);
    double a = -182.036;

    double a_1 = log(pow(y, -sqrt(fabs(x))));
    double a_2 = x - (y / 2.0);
    double a_3 = pow(sin(atan(z)), 2.0);
    double a_4 = a_1 * a_2 + a_3;

    cout << a << endl;
    cout << a_4;
    */

    // 6
    /*
    double x = 16.55 * pow(10.0, -3.0);
    double y = -2.75;
    double z = 0.15;
    double b = -38.902;

    double a_1 = sqrt(10.0 * (pow(x, 1.0/3.0) + pow(x, y + 2.0)));
    double a_2 = pow(asin(z), 2.0) - fabs(x - y);
    double a_3 = a_1 * a_2;
     
    cout << b << endl;
    cout << a_3;
    */

    // 7
    /*
    double x = 0.1722;
    double y = 6.33;
    double z = 3.25 * pow(10.0, -4.0);
    double y_1 = -172.025;

    double a_1 = 5.0 * atan(x);
    double a_2 = 0.25 * acos(x);
    double a_3 = x + 3.0 * fabs(x - y) + pow(x, 2.0);
    double a_4 = fabs(x - y) * z * pow(x, 2.0);
    double a_5 = a_1 - a_2 * (a_3 / a_4);

    cout << y_1 << endl;
    cout << a_5;
    */

    // 8
    /*
    double x = -2.235 * pow(10.0, -2.0);
    double y = 2.23;
    double z = 15.221;
    double f = 39.374;

    double a_1 = exp(fabs(x - y)) * pow(fabs(x - y), x + y);
    double a_2 = atan(x) + atan(z);
    double a_3 = pow((pow(x, 6.0) + pow(log(y), 2.0)), 1.0 / 3.0);
    double a_4 = a_1 / a_2 + a_3;

    cout << f << endl;
    cout << a_4;
    */

    // 9
    /*
    double x = 1.825 * pow(10.0, 2.0);
    double y = 18.225;
    double z = -3.298 * pow(10.0, -2);
    double w = 1.2131;

    double a_1 = fabs(pow(x, y / x) - pow((y / x), 1 / 3));
    double a_2 = y - x;
    double a_3 = cos(y) - (z / (y - x));
    double a_4 = 1 + pow((y - x), 2);
    double a_5 = a_1 + (a_2 * (a_3 / a_4));

    cout << w << endl;
    cout << a_5;
    */

    // 10
    /*
    double x = 3.981 * pow(10.0, -2.0);
    double y = -1.625 * pow(10, 3.0);
    double z = 0.512;
    double a = 1.26185;
    
    double a_1 = pow(2.0, -x);
    double a_2 = sqrt(x + pow(fabs(y), 1.0 / 4.0));
    double a_3 = exp(x - 1.0 / sin(z) / 3.0);
    double a_4 = a_1 * a_2 * a_3;

    cout << a << endl;
    cout << a_4;
    */

    // 11
    /*
    double x = 6.251;
    double y = 0.827;
    double z = 25.001;
    double b = 0.7121;

    double a_1 = pow(y, pow(fabs(x), 1.0 / 3.0));
    double a_2 = pow(cos(y), 3.0);
    double a_3 = fabs(x - y) * (1.0 + ((pow(sin(z), 2.0) / sqrt(x + y))));
    double a_4 = exp(fabs(x - y) + x / 2.0);
    double a_5 = a_1 + (a_2 * (a_3 / a_4));

    cout << b << endl;
    cout << a_5 << endl;
    */

    // 12
    /*
    double x = 3.251;
    double y = 0.325;
    double z = 0.466 * pow(10, -4.0);
    double c = 4.025;

    double a_1 = pow(2.0, pow(y, x));
    double a_2 = pow(pow(3.0, x), y);
    double a_3 = y * atan(z) - (M_PI / 6.0);
    double a_4 = fabs(x) + (1.0 / (pow(y, 2.0) + 1));
    double a_5 = (a_1 + a_2) - (a_3 / a_4);

    cout << c << endl;
    cout << a_5;
    */

    // 13
    /*
    double x = 17.421;
    double y = 10.365 * pow(10, -3.0);
    double z = 0.828 * pow(10, 5.0);
    double f = 0.33056;

    double a_1 = pow(pow(y + (x - 1.0), 1.0 / 3.0), 1.0 / 4.0);
    double a_2 = fabs(x - y) * (pow(sin(z), 2.0) + tan(z));
    double a_3 = a_1 / a_2;

    cout << f << endl;
    cout << a_3;
    */

    // 14
    /*
    double x = 12.3 * pow(10.0, -1.0);
    double y = 15.4;
    double z = 0.252 * pow(10.0, 3.0);
    double g = 82.8257;

    double a_1 = pow(y, x + 1.0);
    double a_2 = pow(fabs(y - 2.0), 1.0 / 3.0) + 3.0;
    double a_3 = x + (y / 2.0);
    double a_4 = 2.0 * fabs(x + y);
    double a_5 = pow(x + 1.0, -1.0 / sin(z));
    double a_6 = (a_1 / a_2) + ((a_3 / a_4) * a_5);

    cout << g << endl;
    cout << a_6;
    */

    // 15
    /*
    double x = 2.444;
    double y = 0.869 * pow(10.0, -2.0);
    double z = -0.13 * pow(10.0, 3.0);
    double h = -0.49871;

    double a_1 = pow(x, y + 1.0) + exp(y - 1.0);
    double a_2 = 1.0 + x * fabs(y - tan(z));
    double a_3 = 1.0 + fabs(y - x);
    double a_4 = (pow(fabs(x - y), 2.0) / 2.0);
    double a_5 = (pow(fabs(x - y), 3.0) / 3.0);
    double a_6 = (a_1 / a_2) * a_3 + a_4 - a_5;

    cout << h << endl;
    cout << a_6;
    */

    // 16
    /*
    double a = 3.7;
    double b = 0.07;
    double c = 1.5;
    double x = 5.75;

    double a_1 = sqrt(c * x);
    double a_2 = 2.7 * (fabs(c) + fabs(x) / (pow(c, 2.0) * pow(x, 2.0)));
    double a_3 = exp(c * x);
    double a_4 = cos(pow(a + b, 2.0) / (c * x - b));
    double y = a_1 - a_2 * a_3 + a_4;

    cout << y;
    */

    // 17
    /*
    double a = 7.5;
    double b = 1.2;
    double x = 0.5;

    double a_1 = 4.5 * (pow(a + b, 2.0) / pow(a - b, 2.0));
    double a_2 = sqrt((a + b) * (a - b));
    double a_3 = pow(10, -1.0) * (log(a - b) / log(a + b) * exp(pow(x, 2.0)));
    double y = a_1 - a_2 + a_3;

    cout << y;
    */

    // 18
    /*
    double a = 5.1;
    double b = 0.7;
    double x = -0.05;
    
    double a_1 = 2.4 * fabs(pow(x, 2.0) + b) / a;
    double a_2 = (a - b) * (pow(sin(a - b), 2.0));
    double a_3 = pow(10.0, -2.0) * (x - b);
    double y = a_1 + a_2 + a_3;

    cout << y;
    */

    // 19
    /*
    double a = 0.1;
    double b = 2.4;
    double x = -0.3;

    double a_1 = (a * x - sqrt(b) / (5.7 * pow(x, 2.0) + pow(b, 2.0)));
    double a_2 = (fabs(x + b) - pow(a, 2.0) / pow(x, 2.0) * (pow(tan(b), 2.0)));
    double y = a_1 - a_2;

    cout << y;
    */

    // 20
    /*
    double a = 4.5;
    double c = 7.4;
    double d = -2.1;
    double x = 0.15;

    double a_1 = sqrt((c - (d * pow(x, 2.0))) / x);
    double a_2 = (log(pow(x, 2.0) + c) / (0.7 * x) + (a + d));
    double a_3 = pow(10, -2.0) / (c - (d * pow(x, 3.0)));
    double y = a_1 + a_2 - a_3;

    cout << y;
    */
}