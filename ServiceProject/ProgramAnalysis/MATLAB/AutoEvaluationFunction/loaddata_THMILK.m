%% File nay de test khong anh huong den function chay tu dong
clear all;
clc;

% Main Location
ProjectPath = 'D:\\Matlab_ChamTuDong\\';

% Test Folder 
THMILK_Folder = [ProjectPath 'THMilk 29022016 Du Lieu Anh'];

% Store step 1 information 
MyDirInfo = dir(THMILK_Folder);
THMILK_AllImageNames = {MyDirInfo.name};

%% Read all Image in Folders
THMILK_AllImages = cell(size(THMILK_AllImageNames,2)-2, 10);

for i = 1 : size(THMILK_AllImages,1)
   disp(['Reading image : ' num2str(i)]);
   THMILK_AllImages{i,1} = THMILK_AllImageNames{i+2};
   THMILK_AllImages{i,2} = [THMILK_Folder '\\' THMILK_AllImages{i,1}];
   THMILK_AllImages{i,3} = imread(THMILK_AllImages{i,2});
end
