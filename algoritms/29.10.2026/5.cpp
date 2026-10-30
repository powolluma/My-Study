#include <iostream>
#include <string>
using namespace std;

class IDE {
private:
    string name;
    int min_ram;
    bool needs_ssd;

public:
    IDE(const string& name, int min_ram, bool needs_ssd) : name(name), min_ram(min_ram), needs_ssd(needs_ssd) {}

    bool canRun(int ram, bool ssd) const {
        return ram >= min_ram && (!needs_ssd || ssd);
    }

    friend ostream& operator<<(ostream& output, const IDE& ide) {
        output << ide.name << " (RAM: " << ide.min_ram << ", SSD: " << (ide.needs_ssd ? "да" : "нет") << ")";
        return output;
    }
};

int main() {
    IDE pycharm("PyCharm", 8, true);
    IDE vscode("VS Code", 4, false);

    cout << pycharm << endl;
    cout << pycharm.canRun(4, false) << endl;
    cout << vscode.canRun(4, false) << endl;

    return 0;
}
