from sys import stdin


def reverseSum(a, b):
    return int(str(int(str(a)[::-1]) + int(str(b)[::-1]))[::-1])


def main():
    n = int(stdin.readline())
    for i in range(0, n):
        a, b = stdin.readline().split()
        print reverseSum(int(a), int(b))


if __name__ == "__main__":
    main()
