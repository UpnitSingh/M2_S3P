#include <iostream>
#include <cstdlib>
#include <chrono>
#include <omp.h>

using namespace std;
using namespace std::chrono;

void randomVector(int *vector, int size) {
    for (int i = 0; i < size; i++) {
        vector[i] = rand() % 100;
    }
}

int main() {
    int size = 1000000;
    srand(42);

    int *v1 = new int[size];
    int *v2 = new int[size];
    int *v3 = new int[size];

    randomVector(v1, size);
    randomVector(v2, size);

    long long total = 0;

    auto start = high_resolution_clock::now();

    #pragma omp parallel for default(none) shared(v1, v2, v3, size, total)
    for (int i = 0; i < size; i++) {
        v3[i] = v1[i] + v2[i];
        #pragma omp critical
        total += v3[i];
    }

    auto stop = high_resolution_clock::now();
    auto duration = duration_cast<milliseconds>(stop - start);

    cout << "Total sum using critical: " << total << endl;
    cout << "Time taken with critical section: " << duration.count() << " ms" << endl;

    delete[] v1;
    delete[] v2;
    delete[] v3;

    return 0;
}
