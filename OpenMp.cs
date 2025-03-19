#include <iostream>
#include <cstdlib>
#include <chrono>
#include <omp.h>

using namespace std;
using namespace std::chrono;

void randomVector(int vector[], int size) {
    for (int i = 0; i < size; i++) {
        vector[i] = rand() % 100;
    }
}

int main() {
    const int size = 1000000;  
    srand(time(0));

    int *v1 = new int[size];
    int *v2 = new int[size];
    int *v3 = new int[size];

    randomVector(v1, size);
    randomVector(v2, size);

    auto start = high_resolution_clock::now();

    // Parallelize with OpenMP
    #pragma omp parallel for
    for (int i = 0; i < size; i++) {
        v3[i] = v1[i] + v2[i];
    }

    auto stop = high_resolution_clock::now();
    auto duration = duration_cast<milliseconds>(stop - start); // Using milliseconds for readability

    cout << "Time taken by OpenMP parallel function: " << duration.count() << " milliseconds" << endl;

    delete[] v1;
    delete[] v2;
    delete[] v3;

    return 0;
}

