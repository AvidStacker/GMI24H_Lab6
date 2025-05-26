# Intrusion Detection via Sorting and Searching Algorithms  
**Laboration 6 – GMI24H: Algorithms and Software Design**

## Overview

This project demonstrates how classic sorting and searching algorithms can be applied to detect patterns in server logs. The context is security-focused: identifying potentially suspicious behavior through data analysis. The system generates synthetic log data and benchmarks the performance of various algorithms.

All implementation is done in C# using .NET and follows best practices for generic programming, benchmarking, and algorithmic analysis.

---

## Lab Objectives

- Implement and analyze the performance of classic sorting and searching algorithms.
- Work with large datasets (up to 10 million entries) to evaluate real-world efficiency.
- Measure and compare performance on sorted vs. unsorted data.
- Identify the best algorithm for a given use case (e.g., rapid lookup, large volume sorting).
- Document and interpret results using test plans and performance charts.

---

## Project Structure

- **AlgorithmLib/**
  - `SortingManager<T>`: Contains implementations of Bubble Sort, Merge Sort, Insertion Sort, Selection Sort, Heap Sort, and Quick Sort.
  - `SearchingManager<T>`: Contains implementations of Linear Search, Binary Search, Exponential Search, Interpolation Search, and Jump Search.
- **GMI24H_VT25_SortSearch_Labb_/**
  - `Benchmark.cs`: Measures execution time and logs results to a CSV file.
  - `Program.cs`: Runs the full test suite and generates performance data.
  - `RandomLogGenerator.cs`: Generates repeatable, randomized log data (IP, timestamp, status code).
  - `LogEntry.cs`: Represents a single row of server log data.

---

## Implemented Algorithms

### Sorting
- Bubble Sort
- Insertion Sort
- Selection Sort
- Merge Sort
- Heap Sort
- Quick Sort

### Searching
- Linear Search
- Binary Search
- Jump Search
- Exponential Search
- Interpolation Search

---

## Test & Benchmark Setup

- **Dataset Size**: 100, 1,000, 10,000, 100,000, 1,000,000 (and 10,000,000 for some cases)
- **Repetitions**: 100 runs per test case
- **Timing Unit**:
  - Sorting: measured in **milliseconds (ms)**
  - Searching: measured in **microseconds (µs)**
- **Test Conditions**:
  - Randomized input
  - Pre-sorted input
  - Search hits at start, middle, end, and missing values

Results are saved in `benchmark_results.csv`.

---

## Deliverables

- ✅ Full algorithm implementation
- ✅ Structured test plans for sorting and searching (filled)
- ✅ Benchmark results file (CSV)
- ✅ Visual performance graphs (created in Excel)
- ✅ Commentary and analysis of test outcomes

---

## Key Learnings

- Real-world performance does not always align with theoretical complexity.
- Input structure (sorted vs. unsorted) has a major impact on naive algorithms like Bubble Sort or Insertion Sort.
- More advanced algorithms like Merge Sort and Quick Sort scale well even at millions of records.
- Search efficiency improves dramatically with sorted data.
