

-- 输出
print("test")

print("------------------全局变量不需要声明,返回nil-------------------")
print(a)
a = 10
print(a)

print("------数据类型:nil, boolean, number, string, function, userdata, thread, table---------------------------")
print(type(10))
print(type(10.3*1))

string1 = "string 1]]"
string2 = [[@ksadfj#[]kj]]
print(string1)
print(string2)

string3 = "字符串连接".."a".."b"
string4 = "3" + "2"
print(string3)
print(string4)

local table1 = {}
local table2 = {"a", "b"}
table2["b"] = 2
table2["c"] = 3
table2["d"] = 3

for k,v in pairs(table2) do
    print(k, v)
end
print("==================")
for i = 1, 10 do
    print(i,table2[i])
end

print("===========function==============")
function testFunc(tab, func)
    for k,v in pairs(tab) do
        print(func(k, v))
    end
end

function testFunc2(k, v)
    print(k, v)
end

tab = {key1 = "val1", key2 = "key2"}
print("=========lamba function=========")
testFunc(tab, 
    function(k,v)
        print(k, v)
    end
)
print("=========function2=========")
testFunc(tab, testFunc2)

print("=========swap=========")
local x = 1
local y = 2
x, y = y, x
print("x="..x, "y="..y)

print("=========get tab=========")
print(tab.key1)

print("=========循环===========")
local count = 3
while(count > 0) do
    count = count - 1
end
print(count)

for i = 1, 3 do
    print("i="..i)
    count = i
end
print(count)

count = 0

repeat
    count = count + 1
until(count == 3)
print(count)


print("=========ipairs=========")
local tab = {"t1", t="tt","t2", "t3", t4="t4", "t4"}
tab[5]="t5"

for i, v in ipairs(tab) do
    print(i, v)
end
print("=========pairs=========")
for i, v in pairs(tab) do
    print(i, v)
end


print("=========流程控制===========")
if(false) then
    print("这里不会进")
elseif(0) then
    print("0 = true")
else
    --do nothing
end

if(1) then
    print("1 = true")
else
    print("1 = false")
end

if(nil) then
    print("nil = true")
else
    print("nil = false")
end

print("=========函数===========")
local function average(...)
    local arg = {...}
    local result = 0
    for i,v in ipairs(arg) do
        result = result + v
    end

    return #arg, result/#arg
end

local num, avg = average(10,5,6,1,1)
print("总共传入"..num.."个参数,平均值为"..avg)
print(10/3)


print("=========运算符===========")
local A, B = true, false
print(A ~= B)
print(A or B)
print(B or A)
print(A and B)
print(not A)
print(2^2)

print("=========字符串操作===========")
print(string.gsub( "aaaaa", "a", "z", 3))
print(string.find( "hello lua , i am coming.", "lua"))
print(string.match( "hello lua , i am coming.", "lua"))

print("=========数组===========")
local array = {}
for i = -2, 2 do
    print(i)
end


print("=========无状态迭代器===========")
local function square(iteratorMaxNumber,currentNumber)
    if(currentNumber <= iteratorMaxNumber) then
        currentNumber = currentNumber + 1
        return currentNumber, currentNumber^2
    end
end

for k,v in square, 3, 1 do
    print(k,v)
end


print("=========迭代器 ipairs===========")
print("需要给for返回 迭代器， 状态常量（不变的），初始值；后面两个其实是用来传给迭代器的")
print("迭代器需要返回 当前key和value")
function iter(tab, i)
    i = i + 1
    local result = tab[i]
    if(result) then
        return i, result
    end
end

function myIpairs(tab)
    return iter, tab, 0
end

for k,v in myIpairs(tab) do
    print(k,v)
end

print("=========多状态迭代器===========")
function elementIterator(collection)
    local index = 0
    local count = #collection

    -- 闭包函数
    return function ()
        index = index + 1
        if index <= count then
            return collection[index]
        end
    end
end

for v in elementIterator(tab) do
    print(v)
end