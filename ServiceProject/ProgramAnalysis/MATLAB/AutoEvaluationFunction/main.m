%% Kiem Tra That Gia : CheckRealImage 
ImagePath = THMILK_AllImages{38,2};
ProjectPath = 'D:\\Matlab_ChamTuDong\\';
BagPath = [ProjectPath 'AutoEvaluationData\\Bag.mat'];
MeanMhistRGBPath = [ProjectPath 'AutoEvaluationData\\MeanMhistRGB.mat'];
RealSamplePath = [ProjectPath 'AutoEvaluationData\\RealImage_Sample.jpg'];

[ Result, Rate, RunningTime, ErrorDesc] = CheckRealImage( ImagePath, BagPath, MeanMhistRGBPath, RealSamplePath);

Set = 3;
IM = cell(1, 10);
DESC = cell(1, 10);
for k = 1 : 10
    i = (Set * 10) + k
    ImagePath = THMILK_AllImages{i,2};
    [ Result, Rate, RunningTime, ErrorDesc] = CheckRealImage( ImagePath, BagPath, MeanMhistRGBPath, RealSamplePath);
    IM{1,k} = THMILK_AllImages{i,3};
    DESC{1,k} = num2str(Result);
end
ShowFigures(2,5, IM, DESC);

%% Kiem Tra Anh Ro Hay Mo : CheckSharpImage 

ImagePath = THMILK_AllImages{38,2};
[ Result, Rate, RunningTime, ErrorDesc ] = CheckSharpImage( ImagePath );

Set = 10;
IM = cell(1, 10);
DESC = cell(1, 10);
for k = 1 : 10
    i = (Set * 10) + k
    ImagePath = THMILK_AllImages{i,2};
    [ Result, Rate, RunningTime, ErrorDesc ] = CheckSharpImage( ImagePath );
    IM{1,k} = THMILK_AllImages{i,3};
    DESC{1,k} = num2str(Result);
end
ShowFigures(2,5, IM, DESC);

%% Kiem Tra Anh Chup Tot hay qua xa qua gan 
ImagePath = THMILK_AllImages{38,2};
[ Result, Rate, RunningTime, ErrorDesc  ] = CheckGoodDistanceImage( ImagePath );

Set = 4;
IM = cell(1, 10);
DESC = cell(1, 10);
for k = 1 : 10
    i = (Set * 10) + k
    ImagePath = THMILK_AllImages{i,2};
    [ Result, Rate, RunningTime, ErrorDesc  ] = CheckGoodDistanceImage( ImagePath );
    IM{1,k} = THMILK_AllImages{i,3};
    DESC{1,k} = num2str(Result);
end
ShowFigures(2,5, IM, DESC);

%% Kiem Tra Anh Chup Co Dung Dia Diem hay khong
VLFeatLibPath = [ProjectPath 'AutoEvaluationData\\VLFeat_Lib\\toolbox\\vl_setup.m'];
ImagePath = THMILK_AllImages{138,2};
OutletImagePath = THMILK_AllImages{141,2};
[ Result, Rate , RunningTime, ErrorStatus, NumCorrelation] = CheckRightOutletImage( ImagePath, OutletImagePath, VLFeatLibPath );

%% Kiem Tra Anh Chup Co San Pham hay khong 
ProductImagePath = [ProjectPath 'AutoEvaluationData\\SuaHop_Sample.jpg'];
ImagePath = THMILK_AllImages{1,2};
[ Result, Rate, RunningTime, ErrorStatus] = CheckProductImage( ImagePath, ProductImagePath);

Set = 1;
IM = cell(1, 10);
DESC = cell(1, 10);
for k = 1 : 10
    i = (Set * 10) + k
    ImagePath = THMILK_AllImages{i,2};
    [ Result, Rate, RunningTime, ErrorStatus] = CheckProductImage( ImagePath, ProductImagePath);
    IM{1,k} = THMILK_AllImages{i,3};
    DESC{1,k} = num2str(Result);
end
ShowFigures(2,5, IM, DESC);

