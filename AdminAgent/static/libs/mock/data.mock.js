Mock.mock(/\/AgentTotalUser\/total_user_AreaList/, {
    "XName":"区域",
    "YName":"用户数量",
    "XYValue":{
        "广州": 100,
        "海口": 10,
        "上海": 1000
    }
});

Mock.mock(/\/AgentTotalOrder\/total_order_AreaList/, {
    "XName":"区域",
    "YName":"订单数量",
    "XYValue":{
        "广州": 100,
        "海口": 10,
        "上海": 1000
    }
});

Mock.mock(/\/AgentTotalOrder\/total_order_TypeList/, {
    "XName":"服务类型",
    "YName":"订单数量",
    "XYValue":{
        "充值": 100,
        "点餐": 10,
        "预定": 1000
    }
});

Mock.mock(/\/AgentTotalOrder\/total_order_AmountList/, {
    "XName":"服务类型",
    "YName":"订单总额",
    "XYValue":{
        "充值": 100,
        "点餐": 10,
        "预定": 1000
    }
});